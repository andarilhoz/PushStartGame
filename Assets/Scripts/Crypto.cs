using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Crypto : MonoBehaviour
{
    public static string SHA256Hash(string text)
    {
        SHA256 sha256 = new SHA256CryptoServiceProvider();
        sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
        byte[] result = sha256.Hash;
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            strBuilder.Append(result[i].ToString("x2"));
        }
        return strBuilder.ToString();
    }
}
