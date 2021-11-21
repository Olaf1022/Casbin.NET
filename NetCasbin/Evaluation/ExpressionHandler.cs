﻿using System;
using System.Collections.Generic;
using Casbin.Caching;
using Casbin.Model;
using DynamicExpresso;

namespace Casbin.Evaluation;

internal class ExpressionHandler : IExpressionHandler
{
    public IExpressionCachePool Cache { get; set; } = new ExpressionCachePool();
    private readonly FunctionMap _functionMap = FunctionMap.LoadFunctionMap();
    private readonly Interpreter _interpreter;

    public ExpressionHandler()
    {
        _interpreter = CreateInterpreter();
    }

    public IDictionary<string, Parameter> Parameters { get; }
        = new Dictionary<string, Parameter>();

    public void SetFunction(string name, Delegate function)
    {
        Cache.Clear();
        _interpreter.SetFunction(name, function);
    }

    public bool Invoke<TRequest, TPolicy>(in EnforceContext context, string expressionString, in TRequest request, in TPolicy policy)
        where TRequest : IRequestValues
        where TPolicy : IPolicyValues
    {
        if (context.View.SupportGeneric is false)
        {
            if (Cache.TryGetFunc<Func<IRequestValues, IPolicyValues, bool>>(expressionString, out var func))
            {
                return func(request, policy);
            }
            func = CompileExpression<IRequestValues, IPolicyValues>(in context, expressionString);
            Cache.SetFunc(expressionString, func);
            return func(request, policy);
        }

        if (Cache.TryGetFunc<Func<TRequest, TPolicy, bool>>(expressionString, out var genericFunc) is not false)
        {
            return genericFunc(request, policy);
        }
        genericFunc = CompileExpression<TRequest, TPolicy>(in context, expressionString);
        Cache.SetFunc(expressionString, genericFunc);
        return genericFunc(request, policy);
    }

    private Func<TRequest, TPolicy, bool> CompileExpression<TRequest, TPolicy>(in EnforceContext context, string expressionString)
        where TRequest : IRequestValues
        where TPolicy : IPolicyValues
    {
        return _interpreter.ParseAsDelegate<Func<TRequest, TPolicy, bool>>(expressionString,
            context.View.RequestType, context.View.PolicyType);
    }

    private Interpreter CreateInterpreter()
    {
        var interpreter = new Interpreter();
        // TODO: Auto deconstruct can not support .NET 4.5.2
        foreach (KeyValuePair<string, Delegate> functionKeyValue in _functionMap.FunctionDict)
        {
            interpreter.SetFunction(functionKeyValue.Key, functionKeyValue.Value);
        }
        return interpreter;
    }
}
