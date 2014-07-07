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
        /// <summary>
        /// Return a Fibonacci number by a given integer
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public long FibonacciNumber(long n)
        {
            // if n > 92 or n < -92, the final Fib number would exceed to long.MaxValue,
            // so we should raise an exception here to tell client the parameter should
            // be in range [-92, 92]
            if((n > 92) || (n < -92))
            {
                string strMsg = string.Format(errorMessageFib, n >= 0 ? ">92" : "<-92");
                throw new FaultException<ArgumentOutOfRangeException>(new ArgumentOutOfRangeException(), strMsg);
            }

            // handle both positive and negative n here
            bool negative = false;
            if (n < 0)
            {
                negative = true;
                n = -n;
            }
            //   F(n) = F(n-1) + F(n-2)   (n > 1)
            //   F(n) = 1                 (n = 1)
            //   F(n) = 0                 (n = 0)
            long a = 0;
            long b = 1;
            for (long i = 0; i < n; i++)
            {
                long temp = a;
                a = b;
                b += temp;               
            }
            //   F(n-2) = F(n) - F(n-1) ==> F(-n) = (-1)^(n+1)F(n) (n > 0)
            if(negative)
            {
                // check whether n is odd or even
                if ((n & 1) == 0)
                    a = -a;
            }
            return a;
            
        }

        /// <summary>
        /// return a reversed string to a given string, for example s = "abc",
        /// then return string is "cba"
        /// </summary>
        /// <param name="s">string</param>
        /// <returns> a reversed string</returns>
        public string ReverseWords(string s)
        {
            //We cannot do reverse for null, raise exception here
            if (null == s)
                throw new FaultException<ArgumentNullException>(new ArgumentNullException(), errorMessageRev);
            string ret = new string(s.ToCharArray().Reverse().ToArray());
            return ret;
        }

        /// <summary>
        /// return the type of triangle thru the input edges' length
        /// </summary>
        /// <param name="a">length of a edge</param>
        /// <param name="b">ength of a edge</param>
        /// <param name="c">ength of a edge</param>
        /// <returns>triange type or error type</returns>
        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            // if one of edges is zero or negative, return error.
            if ((a <= 0) || (b <= 0) || (c <= 0))
                return TriangleType.Error;
            // all edges are equal
            if ((a == b) && (b == c))
                return TriangleType.Equilateral;
            // two edges are equal
            else if (a == b || (b == c) || (a == c))
                return TriangleType.Isosceles;

            TriangleType ret = TriangleType.Error;
            // the edges condition to be a triangle, use minus(-) here to avoid integer overflow by using plus(+)
            if ((a > Math.Abs(b - c)) && (b > Math.Abs(c - a)) && (c > Math.Abs(a - b)))
                ret = TriangleType.Scalene;
            // return error
            return ret;
        }
    }
}
