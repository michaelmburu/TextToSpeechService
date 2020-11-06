using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace TextToSpeechService
{
    class Program
    {
        static async Task Main()
        {
            await SynthesisToSpeakerAsync();
        }

        static async Task SynthesisToSpeakerAsync()
        {
            var config = SpeechConfig.FromSubscription("YOur Subscription Key", "Region");

            using (var synthesizer = new SpeechSynthesizer(config))
            {
                Console.WriteLine("Hello, I'm your assistant. How can I help you today");
                Console.Write("> ");

                string text = Console.ReadLine();

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if(result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesised to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason = {cancellation.Reason}");

                        if(cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        }

                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }



            }
        }
    }
}
