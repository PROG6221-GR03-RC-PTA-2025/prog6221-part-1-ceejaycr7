using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.IO;

class CybersecurityChatbot                                      //Ceejay Alughere - ST104151570
{
    // automatic properties
    public string Name { get; set; }
    public Dictionary<string, string> Responses { get; set; }
    public Dictionary<string, string> CybersecurityTopics { get; set; }
    public ConsoleColor BotColor { get; set; }
    public ConsoleColor UserColor { get; set; }

    public CybersecurityChatbot()
    {
        Name = "CyberGuard";
        BotColor = ConsoleColor.Cyan;
        UserColor = ConsoleColor.Yellow;
        InitializeResponses();
        InitializeCybersecurityTopics();
    }

    private void InitializeResponses()
    {
        Responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"hello", "Hello there! I'm CyberGuard, your cybersecurity assistant. How can I help you today?"},
            {"hi", "Hi! Stay safe online. What cybersecurity topic would you like to discuss?"},
            {"how are you", "I'm a bot, so I'm always at peak performance! Ready to discuss cybersecurity."},
            {"what's your purpose", "My purpose is to educate you about cybersecurity threats like phishing, password safety, and safe browsing."},
            {"what can I ask you", "You can ask me about: \n- Password safety\n- Phishing scams\n- Safe browsing\n- Recognizing malware\n- Social engineering"},
            {"bye", "Goodbye! Remember to stay cyber safe!"},
            {"thanks", "You're welcome! Cybersecurity is everyone's responsibility."},
            {"help", "I can educate you about:\n1. Creating strong passwords\n2. Identifying phishing emails\n3. Safe internet browsing\n4. Protecting personal data\nWhat would you like to know?"},
            {"default", "I didn't quite understand that. Could you rephrase? Or type 'help' to see what I can discuss."}
        };
    }

    private void InitializeCybersecurityTopics()
    {
        CybersecurityTopics = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"phish", "Phishing scams try to trick you into revealing personal info. Watch for:\n- Urgent or threatening language\n- Suspicious sender addresses\n- Requests for passwords or payments\n- Misspellings in emails/websites"},
            {"spot phishing", "Phishing scams try to trick you into revealing personal info. Watch for:\n- Urgent or threatening language\n- Suspicious sender addresses\n- Requests for passwords or payments\n- Misspellings in emails/websites"},
            {"identify phishing", "Phishing scams try to trick you into revealing personal info. Watch for:\n- Urgent or threatening language\n- Suspicious sender addresses\n- Requests for passwords or payments\n- Misspellings in emails/websites"},
            {"phishing email", "Phishing scams try to trick you into revealing personal info. Watch for:\n- Urgent or threatening language\n- Suspicious sender addresses\n- Requests for passwords or payments\n- Misspellings in emails/websites"},
            {"fake email", "Phishing scams often come through emails. Watch for:\n- Generic greetings like 'Dear Customer'\n- Mismatched sender names and addresses\n- Links that don't match the displayed text"},
            {"suspicious email", "When examining suspicious emails:\n- Don't open attachments\n- Hover over links to see real URLs\n- Check for poor grammar/spelling\n- Verify with sender through another channel"},
            {"email scam", "Common email scams include:\n- Fake invoices\n- Prize notifications\n- Account suspension warnings\n- Requests from 'management'\nAlways verify before responding!"},
            {"password", "Strong passwords should:\n- Be at least 12 characters long\n- Include numbers, symbols, and mixed cases\n- Never use personal information\n- Be unique for each account\nConsider using a password manager!"},
            {"browsing", "Safe browsing tips:\n- Look for HTTPS in URLs\n- Don't click suspicious links\n- Use ad-blockers\n- Keep browser updated\n- Avoid public WiFi for sensitive transactions"},
            {"malware", "Malware protection:\n- Install reputable antivirus\n- Don't download from untrusted sites\n- Keep software updated\n- Be cautious with email attachments\n- Regular system scans"},
            {"social engineering", "Social engineering tricks people into breaking security. Be wary of:\n- Unexpected requests for help\n- Too-good-to-be-true offers\n- Impersonation of authority figures\n- Pressure to act quickly"}
        };
    }

    public void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta; // retro purple terminal color
        Console.WriteLine(@"
  ________________________________
 /       * Ceejay Alughere *      \
 |  * CYBER SECURITY ASSISTANT *  |
 \____   ________________    _____/
     |  |                |  |
     |  |                |  |
     |__|________________|__|
");
        Console.ResetColor();
        Console.WriteLine("\nWelcome to your personal cybersecurity assistant!");
        Console.WriteLine("Type 'help' for available commands or 'bye' to exit.\n");

        // I added some retro-style decoration in the header
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("=============================================");
        Console.ResetColor();
    }

    public void PlayWelcomeSound()
    {
        try
        {
            if (File.Exists("welcome.wav"))
            {
                SoundPlayer player = new SoundPlayer("welcome.wav");
                player.Play();
            }
            else
            {
                Console.Beep(440, 500);
            }
        }
        catch
        {
            Console.Beep(440, 500);
        }
    }

    public void TypeWithEffect(string text, int delay = 30)
    {
        Console.ForegroundColor = BotColor;
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
        Console.ResetColor();
    }

    public string GetResponse(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return "Please type something or say 'bye' to exit.";
        }

        string lowerInput = userInput.ToLower();

        // first check for exact phrase matches in cybersecurity topics
        foreach (var topic in CybersecurityTopics)
        {
            if (lowerInput.Contains(topic.Key.ToLower()))
            {
                return topic.Value;
            }
        }

        // then check for general responses
        foreach (var pair in Responses)
        {
            if (lowerInput.Contains(pair.Key.ToLower()))
            {
                return pair.Value;
            }
        }

        return Responses["default"];
    }

    public void StartConversation()
    {
        DisplayHeader();
        PlayWelcomeSound();
        Thread.Sleep(1000);

        while (true)
        {
            Console.ForegroundColor = UserColor;
            Console.Write("\nYou: ");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = BotColor;
                Console.WriteLine($"{Name}: Please type something or say 'bye' to exit.");
                Console.ResetColor();
                continue;
            }

            if (input.Equals("bye", StringComparison.OrdinalIgnoreCase))
            {
                TypeWithEffect($"{Name}: {Responses["bye"]}");
                break;
            }

            string response = GetResponse(input);
            TypeWithEffect($"{Name}: {response}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(Math.Min(100, Console.LargestWindowWidth), Math.Min(30, Console.LargestWindowHeight));

        var chatbot = new CybersecurityChatbot();
        chatbot.StartConversation();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}