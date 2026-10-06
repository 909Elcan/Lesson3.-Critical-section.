namespace ConsoleApp1
{
    internal class Program
    {
      
        const string  CartNumber = "4169 7388 8869 7400";
        private static int _balance = 150;
        private static readonly object _BalaceCode = new object();
        private static void ExecuteLockedWithdrawalProcess()
        {
            lock (_BalaceCode)
            {
                Console.WriteLine("Pulun hansı vasitə ilə çıxarılmasını istəyirsiniz? ");
                Console.WriteLine("1. Mobil Tətbiq");
                Console.WriteLine("2. ATM");
                Console.Write("Seçiminiz (1 və ya 2): ");
                string secim = Console.ReadLine();

                if (secim == "1" || secim =="2")
                {
                    Console.WriteLine("Cixarmaq istediyiniz meblegi qeyid edin: ");
                    if (int.TryParse(Console.ReadLine(), out int moneybank) && moneybank > 0)
                    {
                        if (_balance >= moneybank)
                        {
                            Interlocked.Add(ref _balance, -moneybank);
                            Console.WriteLine($" Məbləğ çıxarıldı: {moneybank} AZN Qalan balans: {_balance} AZN");
                        }
                        else
                        {
                            Console.WriteLine($" Balansda kifayət qədər vəsait yoxdur! ");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Düzgün məbləğ daxil edilmədi!");
                    }

                }
                else
                {
                    Console.WriteLine("Seçim düzgün deyil. Lütfən 1 və ya 2 daxil edin.");
                    return;
                }

            }

        }
            static void Main(string[] args)
            {
                Console.Write("16 kart daxil edin: ");
                string enteredPin = Console.ReadLine();

                if(enteredPin == CartNumber)
                {
                    Thread transactionThread = new Thread(ExecuteLockedWithdrawalProcess);
                    transactionThread.Start();
                    transactionThread.Join();

                    Thread thread2 = new Thread(ExecuteLockedWithdrawalProcess);
                    thread2.Start();
                    thread2.Join(); 
                Console.WriteLine($"\n Yekun balans: {_balance} AZN");
            }
                else
                {
                    Console.WriteLine("Kart kodu sef daxil edildi !!!");
                    return;
                }
   
           }


        
    }
}
