﻿namespace Levinor.Business.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }             
        public Password Password { get; set; }
    }
}
