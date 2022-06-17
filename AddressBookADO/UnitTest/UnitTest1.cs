using NUnit.Framework;

namespace AddressBookADO
{
    public class Tests
    {
        AddressBook addressBook;
        [SetUp]
        public void Setup()
        {
            addressBook = new AddressBook();
        }
        /// <summary>
        /// TC 1: Test Method to check if the contact is added to address book  using stored procedure
        /// </summary>
        [Test]
        public void TestMethodInsertIntoTable()
        {
            int expected = 1;
            Contact contact = new Contact();
            contact.Firstname = "Rajvardhan";
            contact.Lastname = "Singh";
            contact.Address = "Clement Town";
            contact.City = "Dehradun";
            contact.State = "Uttrakhand";
            contact.Zip = 248002;
            contact.PhoneNumber = 8439560765;
            contact.Email = "rajvardhan.2627@gmail.com";
            contact.Type = "Friends";
            int actual = addressBook.InsertIntoTable(contact);
            Assert.AreEqual(expected, actual);
        }
    }
}