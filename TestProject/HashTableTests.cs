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

        [Fact]
        public void Get_UnknownKey_ShouldThrow()
        {
            var table = new HashTable<string, int>(10);
            Assert.Throws<KeyNotFoundException>(() => table.Get("missing"));
        }

        [Fact]
        public void Collision_ShouldStillStoreBothItems()
        {
            // force collisions by using size 1 (alles in dezelfde bucket)
            var table = new HashTable<string, int>(1);
            table.Put("A", 1);
            table.Put("B", 2);

            Assert.Equal(1, table.Get("A"));
            Assert.Equal(2, table.Get("B"));
        }
    }
}
