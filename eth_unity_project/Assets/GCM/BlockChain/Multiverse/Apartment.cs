using System;
using System.Numerics;

public class Apartment
{
    public string tokenId;
    public string owner;
    public string name;
    public string description;
    public string image;
    public string media;
    public Attribute[] attributes;


    public Apartment Clone()
    {
        Apartment newCS = new Apartment();
        // TODO - complete this
        return newCS;
    }

    public class Attribute
    {
        public string trait_type;
        public string value;
    }
}

