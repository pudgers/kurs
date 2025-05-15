using Ollama;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs
{
   public class OllamaController
    {
        OllamaApiClient client;
        Chat Chat;

        public async Task<string> QueryAsync(String symptoms,String name,String tarif)
        {
            var prompt = File.ReadAllText("ai\\prompt.txt");
            prompt = prompt.Replace("{symptoms}", symptoms);
            prompt = prompt.Replace("{name}", name);
            prompt = prompt.Replace("{tarif}", tarif);
            var data = await client.Completions.GenerateCompletionAsync("gemma3:1b", prompt,stream:false);
            return data.Response;
        
        }


        public async Task<string> SendMessage(String text)
        {
            var data = await Chat.SendAsync(text,MessageRole.User);
            return data.Content;

        }

        public async Task<string> SendPrompt(String symptoms, String name, String tarif)
        {
            var prompt = File.ReadAllText("ai\\prompt.txt");
            prompt = prompt.Replace("{symptoms}", symptoms);
            prompt = prompt.Replace("{name}", name);
            prompt = prompt.Replace("{tarif}", tarif);
            var data = await Chat.SendAsync(prompt, MessageRole.System);
            return data.Content;

        }
        public OllamaController()
        {
            client = new OllamaApiClient();
            Chat = client.Chat("gemma3:1b");
        }



    }
}
