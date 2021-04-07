using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 工厂模式就是把创建对象的过程交给工厂类去做，而不是由我们需要用对象的人去创建，我们只管使用，不管这个对象是怎样创建出来的。 
 */
public class SimpleFactoryModel_1 : MonoBehaviour
{
   
    void Start()
    {
        Operation op = CreateOperationFactory.GetOperation("/");
        op.numA = 1f;
        op.numB = 2f;
        Debug.Log(op.GetResult());
    }
    
}

public class CreateOperationFactory
{
    public static Operation GetOperation(string operation)
    {
        switch (operation)
        {
            case "+":
                return new AddFun();
            case "-":
                return new SubtractionFun();
            case "*":
                return new MultiplicationFun();
            case "/":
                return new DivisionFun();
            default:
                return null;
        }
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
