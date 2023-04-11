﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickedKnowledgeHub.Model
{
    public class Employee
    {
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public EmployeeType Type { get; set; }

        public Employee(string mail, string name, string nickname, string password, EmployeeType type)
        {
            Mail = mail;
            Name = name;
            Nickname = nickname;
            Password = password;
            Type = type;
        }
        //Fatter ikke helt hvorfor vi har denne????
        public Employee(string mail, string name, string password, EmployeeType type)
        {
            Mail = mail;
            Name = name;
            Password = password;
            Type = type;
        }
    }
}
