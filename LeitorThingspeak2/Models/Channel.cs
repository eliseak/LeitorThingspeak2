﻿using System;

/// <summary>
/// Classe que guarda as informações do canal do ThingSpeak 
/// </summary>

namespace LeitorThingspeak2
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public Int64 Last_entry_id { get; set; }

        // Sobrecarga 
        public string GetValueFromField(int fieldNumber)
            => GetValueFromField(fieldNumber.ToString());

        /* 
         *  Método que encontra o NOME do campo utilizando o número do campo como referência.
         *  Um canal pode ter de 1 ~ 8 campos.
         */
        public string GetValueFromField(string fieldNumber)
            => Utils.PropValueSearcher.ByName(this, "Field" + fieldNumber).ToString();

        public override string ToString()
        {
            return "id=" + Id +
                    ",name=" + Name +
                    ",description=" + Description +
                    ",latitude=" + Latitude +
                    ",longitude=" + Longitude +
                    ",field1=" + Field1 +
                    ",field2=" + Field2 +
                    ",field3=" + Field3 +
                    ",field4=" + Field4 +
                    ",field5=" + Field5 +
                    ",field6=" + Field6 +
                    ",field7=" + Field7 +
                    ",field8=" + Field8 +
                    ",created_at=" + Created_at +
                    ",updated_at=" + Updated_at +
                    ",last_entry_id=" + Last_entry_id;
        }
    }
}