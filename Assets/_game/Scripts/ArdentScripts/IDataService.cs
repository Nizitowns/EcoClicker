using System.IO;
public interface IDataService
{
 bool SaveData<T>(string RelativePath, T Data, bool isEncrypted);
 
 T LoadData<T>(string RelativePath, bool isEncrypted);
 
}
