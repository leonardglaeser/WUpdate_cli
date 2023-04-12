// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using WUApiLib;
Console.WriteLine("WUpdate_cli");

ISystemInformation systemInfo = new SystemInformation();

Console.WriteLine("Reboot required: " + systemInfo.RebootRequired);

IAutomaticUpdates automaticUpdates = new AutomaticUpdates();
Console.WriteLine("Auto-updates enabled: " + automaticUpdates.ServiceEnabled);

IUpdateSession  session = new UpdateSession();
IUpdateSearcher  searcher = session.CreateUpdateSearcher();

Console.WriteLine("\n\nUpdates assigned\n\n");

ISearchResult result = searcher.Search("DeploymentAction='Installation' AND IsAssigned=1");

foreach (IUpdate update in result.Updates)
{
    Console.WriteLine(update.Title+"\n" +update.KBArticleIDs + "\n\n"+update.Description+ "\n\n");
}


Console.WriteLine("\n\n\nUpdates installed\n");

IUpdateHistoryEntryCollection historyEntryCollection = searcher.QueryHistory(0,searcher.GetTotalHistoryCount());
foreach (IUpdateHistoryEntry historyEntry in historyEntryCollection)
{
    Console.Write(historyEntry.Title + historyEntry.Date + "\n" +historyEntry.Description + "\n" + historyEntry.UpdateIdentity.UpdateID + "\n\n");
}

Console.WriteLine("Press any Key to end");
Console.ReadKey();