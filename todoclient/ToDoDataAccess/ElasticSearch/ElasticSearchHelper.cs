using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using Nest;
using ToDoDataAccess.Entities.ElasticSearch;
using Task = System.Threading.Tasks.Task;

namespace DAL.ElasticSearch
{
    public class ElasticSearchHelper
    {
        private readonly ElasticClient _elasticClient = new ElasticClient(new ConnectionSettings(
        new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"])));

        public void CreateIndex()
        {
            _elasticClient.CreateIndex("todorepository", set => set
            .Settings(s => s
                .Analysis(descriptor => descriptor
                        .Tokenizers(token => token
                            .NGram("customNGramTokenizer", ng => ng
                                .MinGram(1)
                                .MaxGram(15)
                                .TokenChars(TokenChar.Letter, TokenChar.Digit)
                            )
                        )
                    .Analyzers(bases => bases
                            .Custom("customIndexNgramAnalyzer", cia => cia
                                .Filters("lowercase")
                                .Tokenizer("customNGramTokenizer")
                            )
                            .Custom("customSearchNgramAnalyzer", csa => csa
                                .Filters("lowercase")
                                .Tokenizer("keyword")
                            )
                        )
                ))
                .Mappings(ms => ms
                    .Map<Task>(m => m
                        .AutoMap())
                    .Map<User>(m => m
                        .AutoMap()))
                    );
        }
    }
}
