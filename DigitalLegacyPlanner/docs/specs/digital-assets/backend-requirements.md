# Digital Assets - Backend Requirements

## API Endpoints

#### POST /api/assets/document
Document valuable digital asset
- **Request Body**: `{ assetType, location, value, accessMethod, beneficiaryId }`
- **Events**: `DigitalAssetDocumented`

#### POST /api/assets/intellectual-property
Log intellectual property
- **Request Body**: `{ workDescription, copyrightInfo, licensing, intendedHeir }`
- **Events**: `IntellectualPropertyLogged`

#### POST /api/assets/cryptocurrency
Record cryptocurrency wallet
- **Request Body**: `{ cryptoType, walletAddress, accessMethod, recoveryPhraseLocation }`
- **Events**: `CryptocurrencyWalletRecorded`

#### POST /api/assets/photo-library
Document photo library
- **Request Body**: `{ storageLocation, sizeGB, preservationPriority, accessInstructions }`
- **Events**: `DigitalPhotoLibraryDocumented`

## Data Models

```csharp
public class DigitalAsset
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AssetType AssetType { get; set; }
    public string Location { get; set; }
    public decimal EstimatedValue { get; set; }
    public string AccessMethod { get; set; }
    public Guid? BeneficiaryId { get; set; }
}

public class CryptocurrencyWallet
{
    public Guid Id { get; set; }
    public string CryptoType { get; set; }
    public string WalletAddress { get; set; }
    public string EncryptedRecoveryPhrase { get; set; }
    public string AccessInstructions { get; set; }
}
```
