using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[assembly: ContractNamespaceAttribute("http://KnockKnock.readify.net", ClrNamespace = "knockknock.readify.net")]

namespace knockknock.readify.net
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://KnockKnock.readify.net")]
    public interface IRedPill
    {
        [OperationContract]
        ContactDetails WhoAreYou();

        [OperationContract]
        [FaultContract(typeof(ArgumentOutOfRangeException))]
        long FibonacciNumber(long n);

        [OperationContract]
        [FaultContract(typeof(ArgumentNullException))]
        string ReverseWords(string s);

        [OperationContract]
        TriangleType WhatShapeIsThis(int a, int b, int c);
    }

    [DataContract]
    public enum TriangleType
    {
        [EnumMember]
        Error = 0,
        [EnumMember]
        Equilateral = 1,
        [EnumMember]
        Isosceles = 2,
        [EnumMember]
        Scalene = 3,
    }

    [DataContract]
    public class ContactDetails
    {
        private string EmailAddressField = "xaeroyuan@gmail.com";
        private string FamilyNameField = "Yuan";
        private string GivenNameField = "Shaohua";
        private string PhoneNumberField = "+61-(04)-2364-9009";

        [DataMember]
        public string EmailAddress
        {
            get { return this.EmailAddressField; }
            set { this.EmailAddressField = value; }
        }

        [DataMember]
        public string FamilyName
        {
            get { return this.FamilyNameField; }
            set { this.FamilyNameField = value; }
        }

        [DataMember]
        public string GivenName
        {
            get { return this.GivenNameField; }
            set { this.GivenNameField = value; }
        }

        [DataMember]
        public string PhoneNumber
        {
            get { return this.PhoneNumberField; }
            set { this.PhoneNumberField = value; }
        }
    }
}
