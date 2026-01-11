using CustomAlgoritmen.HashTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class HashTableTests
    {
        [Fact]
        public void PutAndGet_ShouldWork()
        {
            var table = new HashTable<string, int>(10);
            table.Put("Student1", 8);
            table.Put("Student2", 6);

            Assert.Equal(8, table.Get("Student1"));
            Assert.Equal(6, table.Get("Student2"));
        }

        [Fact]
        public void DuplicateKey_ShouldOverwrite()
        {
            var table = new HashTable<string, string>(10);
            table.Put("Kleur", "Rood");
            table.Put("Kleur", "Blauw");

            Assert.Equal("Blauw", table.Get("Kleur"));
        }
    }
}
