﻿namespace Ope_OrderClient
{
    public class Customer
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public override string ToString()
        {
            return $"{id} - {firstName} - {lastName}";
        }
    }
}
