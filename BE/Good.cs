using System;
using System.Collections.Generic;
using System.Text;

namespace BELayer
{
    public class Good
    {
        public enum GoodType { Nutrition, Clothes, Communication, Cleaning, Infants, Pets };
        private string Id;
        private float price;
        private string Shop;
        private float Quantity;
        private string Picture;
        private string Description;
        private GoodType Type;

        public Good(string id, float price, string shop, float quantity, string picture, string description, GoodType type, string name)
        {
            Id = id;
            this.price = price;
            Shop = shop;
            Quantity = quantity;
            Picture = picture;
            Description = description;
            Type = type;
            Name = name;
        }

        public Good(string id, float price, string shop, float quantity, GoodType type, string name)
        {
            Id = id;
            this.price = price;
            Shop = shop;
            Quantity = quantity;
            Picture = null;
            Description = null;
            Type = type;
            Name = name;
        }

        private string Name { get; set; }
        public string getID()
        {
            return Id;
        }
        public bool PictureIsNull()
        {
            return Picture == null;
        }

        public bool DescriptionIsNull()
        {
            return (Description == null) || (Description.Length == 0);
        }


    }
}
