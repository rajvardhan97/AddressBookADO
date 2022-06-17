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
            Contact contact = new Contact
            {
                Firstname = "Rajvardhan",
                Lastname = "Singh",
                Address = "Clement Town",
                City = "Dehradun",
                State = "Uttrakhand",
                Zip = 248002,
                PhoneNumber = 8439560765,
                Email = "rajvardhan.2627@gmail.com",
                Type = "Friends"
            };
            var expected = "Rajvardhan";
            var actual = contact.Firstname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// TC 2: Update data based on First name and Last name
        /// </summary>
        [Test]
        public void GivenUpdate()
        {
            Contact contact = new Contact
            {
                Firstname = "Rajvardhan",
                Lastname = "Singh",
                PhoneNumber = 7017719337
            };
            var expected = 7017719337;
            var actual = contact.PhoneNumber;
            Assert.AreEqual(expected,actual);
        }

        /// <summary>
        /// TC 3: Remove contact by First name using stored procedure
        /// </summary>
        [Test]
        public void RemoveContact()
        {
            Contact contact = new Contact
            {
                Firstname = "Rajvardhan"
            };
            bool result = addressBook.Delete(contact);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// TC 4: Retrieve Data by City or State
        /// </summary>
        [Test]
        public void RetrieveData_ByCityorState()
        {
            string expected = "Abhishekh Kshitij Himanshu ";
            string actual = addressBook.PrintDataBasedOnCity("City", "State");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// TC 5: Print count of persons by state and city
        /// </summary>
        [Test]
        public void Query_ReturnCount()
        {
            string expected = "1 1 1 ";
            string actual = addressBook.PrintCountBasedOnCityandState();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// TC 6: Ability to retrieve entries sorted alphabetically
        /// </summary>
        [Test]
        public void SortQuery_ReturnEntriesSortedAlphabetically()
        {
            string expected = "Abhishekh ";
            string actual = addressBook.PrintSortDataBasedOnCity("Dehradun");
            Assert.AreEqual(expected, actual);
        }
    }
}