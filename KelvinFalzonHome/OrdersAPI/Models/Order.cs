﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrdersAPI.Models
{
	public class Order
	{
		//[BsonId]
		//[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string? UserId { get; set; }
        public string MovieId { get; set; }
        public string Genre { get; set; }

    }
}
