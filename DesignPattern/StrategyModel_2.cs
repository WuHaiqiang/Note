using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
一、策略模式的定义：
它定义了算法家族，分别封装起来，让它们之间可以互相替换，此模式让算法的变化，不好影响到使用算法的客户。
大概意思就是一个对象由于实例化不同的策略，所以最终在调用这个对象的统一方法的所获得的结果不不同。
其实策略模式就是一个算法接口（Vehicle）,多个算法实现（Bicycle，Bus,Car）,和一个上下文（Person）。
二、策略模式和工厂模式的区别：
1.它们的用途不一样。简单工厂模式是创建型模式,它的作用是创建对象。策略模式是行为型模式,作用是在许多行为中选择一种行为,关注的是行为的多样性。
2.解决的问题不同。简单工厂模式是解决资源的统一分发,将对象的创立同客户端分离开来。策略模式是为了解决策略的切换和扩展。
 */
public class StrategyModel : MonoBehaviour
{
    void Start()
    {
        //策略
        Person person = new Person(new AddFun());
        Debug.Log(person.GetResult());
        //策略和简单工厂结合
        Person_1 person_1 = new Person_1("+");
        Debug.Log(person_1.GetResult());
    }
}

public class Person
{
    private Operation op;
    public Person(Operation op)
    {
        this.op = op;
    }

    public float GetResult()
    {
        return this.op.GetResult();
    }
}

public class Person_1
{
    private Operation op;
    public Person_1(string oper)
    {
        switch (oper)
        {
            case "+": op = new AddFun(); break;
            case "-": op = new SubtractionFun(); break;
            case "*": op = new MultiplicationFun(); break;
            case "/": op = new DivisionFun(); break;
            default: op = null; break;
        }
    }
    public float GetResult()
    {
        return this.op.GetResult();
    }
}
public abstract class Operation
{
    public float numA;
    public float numB;

    public abstract float GetResult();
}
public class AddFun : Operation
{
    public override float GetResult()
    {
        return numA + numB;
    }
}
public class SubtractionFun : Operation
{
    public override float GetResult()
    {
        return numA - numB;
    }
}
public class MultiplicationFun : Operation
{
    public override float GetResult()
    {
        return numA * numB;
    }
}
public class DivisionFun : Operation
{
    public override float GetResult()
    {
        if (numB != 0)
            return numA / numB;
        else
            return 0;
    }
}
