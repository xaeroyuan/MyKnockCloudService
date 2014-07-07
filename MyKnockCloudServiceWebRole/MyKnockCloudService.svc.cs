using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace knockknock.readify.net
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://KnockKnock.readify.net")]
    public class RedPill : IRedPill
    {
        private const string errorMessageFib = "Fib({0}) will cause a 64-bit integer overflow.\r\nParameter name: n";
        private const string errorMessageRev = "Value cannot be null.\r\nParameter name: s";
        public ContactDetails WhoAreYou()
        {
            ContactDetails cd = new ContactDetails();
            cd.EmailAddress = "xaeroyuan@gmail.com";
            cd.FamilyName = "Yuan";
            cd.GivenName = "Shaohua";
            cd.PhoneNumber = "0423649009";
            return cd;
        }

        public long FibonacciNumber(long n)
        {
            
            if((n > 92) || (n < -92))
            {
                string strMsg = string.Format(errorMessageFib, n >= 0 ? ">92" : "<-92");
                throw new FaultException<ArgumentOutOfRangeException>(new ArgumentOutOfRangeException(), strMsg);
            }
            bool negative = false;
            if (n < 0)
            {
                negative = true;
                n = -n;
            }
                
            long a = 0;
            long b = 1;
            for (long i = 0; i < n; i++)
            {
                long temp = a;
                a = b;
                b = temp + b;
            }
            if(negative)
            {
                if ((n & 1) == 0)
                    a = -a;
            }
            
            return a;
            
        }

        public string ReverseWords(string s)
        {
            if (null == s)
                throw new FaultException<ArgumentNullException>(new ArgumentNullException(), errorMessageRev);
            string ret = new string(s.ToCharArray().Reverse().ToArray());
            return ret;
        }

        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            if ((a <= 0) || (b <= 0) || (c <= 0))
                return TriangleType.Error;
            if ((a == b) && (b == c))
                return TriangleType.Equilateral;
            else if (a == b || (b == c) || (a == c))
                return TriangleType.Isosceles;

            TriangleType ret = TriangleType.Error;
            if ((a > Math.Abs(b - c)) && (b > Math.Abs(c - a)) && (c > Math.Abs(a - b)))
                ret = TriangleType.Scalene;

            return ret;
        }
    }
}
