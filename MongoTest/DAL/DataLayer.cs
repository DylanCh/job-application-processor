using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoTest.Models;
using System.Linq;

namespace MongoTest.DAL{
    public class DataLayer{
        private static MongoClient client = new MongoClient("mongodb://localhost:27017");
        
        
    /*
     * Complete the function below.
     */
    static int[] sort_hotels(string keywords, int[] hotel_ids, string[] reviews) {
        var kwords = keywords.Split(' ');
        var dict = new Dictionary<int, int>();
        var dict2 = new Dictionary<int, int>();
        
        for(var i = 0; i< hotel_ids.Length; i++){
            
            var countWords = 0;
            foreach(var k in kwords){
                if (reviews[i].Contains(k))
                    countWords++;
            }
            // If dict2 does not already has the hotel review
            if (!dict2.ContainsKey(hotel_ids[i]))
                dict2.Add(hotel_ids[i],countWords);
            //else add matching keyword counts
            else dict2[hotel_ids[i]]+=countWords;
        }
        
        //dict = dict2.ToList().OrderBy(o=>o.Value).ToDictionary(p=>p.Key, p=>p.Value);
        //dict = from entry in dict2 orderby entry.Value descending select entry;
        return new List<int>(dict.Keys).ToArray();
        
    }


        public void Insert(Applicant applicant){
            var applicants = client.GetDatabase("e-library").GetCollection<Applicant>("Applicants");
            applicants.InsertOne(new Applicant(){
                name = applicant.name,
                RoleType = applicant.RoleType,
                languages = applicant.languages
            });
        }
        
        public IEnumerable<Applicant> FindAll(){
            var collection = client.GetDatabase("e-library").GetCollection<Applicant>("Applicants");
            var doc = collection.Find(new BsonDocument()).ToList();
            return doc;
        }

        public IEnumerable<Applicant> Find(string language){
            var collection = client.GetDatabase("e-library").GetCollection<Applicant>("Applicants");
            
            var filter = Builders<Applicant>.Filter
                //.Regex("x", new BsonRegularExpression(language, "i"));
                .AnyEq(x=>x.languages,language);
            var projection = Builders<Applicant>.Projection.Exclude("id");
            var doc = collection.Find(filter: filter).ToList();
            return doc;
        }

        // public static async Task Find()
        // {
        //     var db = client.GetDatabase("e-library");
        //     // var eLibrary = db.GetCollection<BsonDocument>("e-library");
        //     // await Find(eLibrary);
        //     var applicants = db.GetCollection<Applicants>("Applicants");
        //     await Find(applicants);
        // }

        // private static async Task<List<Applicants>> Find(IMongoCollection<Applicants> applicants){
        //     using (var cursor = await applicants.FindAsync(new Applicants())){
        //         var list = new List<Applicants>();
        //         while (await cursor.MoveNextAsync()){
        //             var cur = cursor.Current;
        //             foreach(var item in cur){
        //                 list.Add(item);
        //             }
        //         }
        //         return list;
        //     }
        // }

        // private static async Task<List<BsonDocument>> Find(IMongoCollection<BsonDocument> eLibrary)
        // {
        //     using (var cursor = await eLibrary.FindAsync(new BsonDocument()))
        //     {
        //         var list = new List<BsonDocument>();
        //         while (await cursor.MoveNextAsync())
        //         {
        //             var cur = cursor.Current;
        //             foreach(var item in cur){
        //                 System.Diagnostics.Debug.WriteLine(item);
        //                 list.Add(item);
        //             }

        //         }
        //         return list;
        //     }
        // }
    }
}