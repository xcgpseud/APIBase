﻿using Domain.Models.Interfaces;
using Domain.Validators;
using FluentValidation.Attributes;

namespace Domain.Models
{
    [Validator(typeof(UserValidator))]
    public class User : IModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}