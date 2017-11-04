using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTest.Models{
    [BsonIgnoreExtraElements]
    [Serializable]
    public class Applicant {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("RoleType")]
        public int RoleType { get; set; }
        [BsonElement("languages")]
        public List<string> languages { get; set; }
    }
}