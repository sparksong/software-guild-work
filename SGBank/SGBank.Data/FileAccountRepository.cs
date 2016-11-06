using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        const string path = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\SGBank\SGBank.Data\bin\Debug\Accounts.txt";

        private List<Account> GetAccountsFromFile()
        {
            List<Account> accounts = new List<Account>();
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Account newAccount = new Account();
                        string[] columns = line.Split('|');

                        newAccount.AccountNumber = columns[0];
                        newAccount.Name = columns[1];
                        newAccount.Balance = decimal.Parse(columns[2]);

                        switch (columns[3])
                        {
                            case "F":
                                newAccount.Type = AccountType.Free;
                                break;
                            case "B":
                                newAccount.Type = AccountType.Basic;
                                break;
                            case "P":
                                newAccount.Type = AccountType.Premium;
                                break;
                            default:
                                throw new Exception($"Error reading from file: account {newAccount.AccountNumber} is not a supported type.");
                        }
                        accounts.Add(newAccount);
                    }
                }
            }
            return accounts;
        }

        public Account LoadAccount(string AccountNumber)
        {
            foreach (var account in GetAccountsFromFile())
            {
                if (account.AccountNumber == AccountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            List<Account> accounts = GetAccountsFromFile();

            using (StreamWriter writer = new StreamWriter(path))
            {
                string accountTypeString = string.Empty;
                writer.WriteLine("AccountNumber|Name|Balance|Type");
                foreach (Account existingAccount in accounts)
                {
                    if (existingAccount.AccountNumber == account.AccountNumber)
                    {
                        existingAccount.Balance = account.Balance;
                    }
                    switch (existingAccount.Type)
                    {
                        case AccountType.Free:
                            accountTypeString = "F";
                            break;
                        case AccountType.Basic:
                            accountTypeString = "B";
                            break;
                        case AccountType.Premium:
                            accountTypeString = "P";
                            break;
                        default:
                            throw new Exception($"Error returning the account type.");
                    }
                   
                    writer.WriteLine("{0}|{1}|{2}|{3}", existingAccount.AccountNumber, existingAccount.Name, existingAccount.Balance, accountTypeString);
                }
            }
        }
    }
}