using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseDTO<T>
{
    public bool success;
    public T data;
    public string custMsg;
    public string errMsg;

}
