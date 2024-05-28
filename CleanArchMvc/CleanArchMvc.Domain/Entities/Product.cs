﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id > 0, "Invalid id value");
            Id = id;

            ValidateDomain(name, description, price, stock, image);
        }

        // Metodo para permitir atualizar os dados do produto
        public void Update(string name, string description, decimal price, int stock, string image, int catedoryID)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = catedoryID;
        }


        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid description. Is required");
            DomainExceptionValidation.When(description.Length < 5, "Invalid description, to short, minimum 5 characters");

            DomainExceptionValidation.When(price < 0, "Invalid price value");

            DomainExceptionValidation.When(stock < 0, "Invalid stock value");

            DomainExceptionValidation.When(image.Length > 250, "Invalid image name, maximum 250 characters");

            // Caso não caia em nenhuma das validações acima, atribui os valores na propriedade
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;

        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
