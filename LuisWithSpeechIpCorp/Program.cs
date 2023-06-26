using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Intent;
using Newtonsoft.Json.Linq;

namespace LuisWithSpeechIpCorp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Diga algo...");
                ObterIntencao();
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static  async void ObterIntencao()
        {
            var config = SpeechConfig.FromSubscription("a3557de3a21742f797871ef49da4ab5a", "westus");
            config.SpeechRecognitionLanguage = "pt-BR";

            using (var recognizer = new IntentRecognizer(config))
            {
                var model = LanguageUnderstandingModel.FromAppId("290f6f2e-5823-4fa3-bedd-649e64ab3dae");
                //recognizer.AddIntent(model, "InformacaoSobreProduto", "apelidoIntencao");
                recognizer.AddAllIntents(model);

                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);
                if (result.Reason == ResultReason.RecognizedIntent)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Texto Transcrito={result.Text}");
                    Console.WriteLine($"Intenção Identificada: {result.IntentId}.");

                    if (result.IntentId== "InformacaoSobreProduto")
                    {
                        try
                        {
                            string jsonRetorno = result.Properties.GetProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult);
                            Retorno retorno = new Retorno(jsonRetorno);
                            string produto = retorno.Entidades.FirstOrDefault().Resolution.Values.FirstOrDefault();
                            Console.WriteLine($"Produto Identificado: {produto}.");
                            ResponderProduto(produto);
                        }
                        catch
                        {
                            Console.WriteLine($"Não consegui entender o que você quer!");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Não consegui entender o que você quer!");
                    }


                }
                else 
                {
                    Console.WriteLine($"Houve um problema no reconhecimento!");
                }
            }
        }

        private static void ResponderProduto(string produto)
        {
            Console.WriteLine();
            Console.WriteLine("Descrição do produto:");
            switch (produto)
            {
                case "EpbxManager":
                    Console.WriteLine("O TalkEPBX é um sistema completo de telecomunicações que tem como objetivo gerenciar e operacionalizar todo o sistema de telefonia da sua empresa. Este sistema pode ser chamado de all-in-one por possui, além das funcionalidades de um PABX convencional e de um CTI (Computer Telephony Integration), conta como aliado um poderoso DAC (Distribuidor Automático de Chamadas). Todos esses serviços integrados com URA, acesso ao banco de dados, Discador Automático, Gravação, Conferência, Correio de Voz, FAX, além das opções tradicionais para o dia a dia de um Contact Center.");
                    break;
                case "Miner":
                    Console.WriteLine("O TalkMiner minera!");
                    break;
                case "TalkSMS":
                    Console.WriteLine("A sua empresa em contato com o mundo.O serviço de SMS é muito versátil, com várias possibilidades de utilização para o seu negócio.");
                    break;
                case "i91":
                    Console.WriteLine("O i91 é o novo produto da IPCorp Telecom, possui sistema completo de gestão, controle e operação de telecomunicações. Ideal para empresas que necessitam ter maior produtividade em sua área de vendas e administrativa.");
                    break;
            }
        }

    }





}
