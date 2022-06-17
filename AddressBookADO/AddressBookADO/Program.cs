using System;
namespace AddressBookADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AddressBook addressBook = new AddressBook();
            addressBook.SetConnection();
            addressBook.PrintCountBasedOnCityandState();
        }
    }
}