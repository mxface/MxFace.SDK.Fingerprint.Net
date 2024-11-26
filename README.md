# **MxFace Fingerprint Software Documentation**

## **Prerequisites**
Before using the services, ensure the following:
- Windows Services are running on both web & server.
- Your device is connected to your system.
- The project framework is .NET 6.0 or above.

## **Interfaces**

### **IDevice**
The `IDevice` interface manages interactions with fingerprint devices.

#### **Methods**:

- **`GetConnectedDevicesAsync(List<string> devices)`**
  - **Purpose**: Retrieves the number of connected fingerprint devices.
  - **Parameters**:
    - `List<string> devices`: A list of device names or identifiers, populated during the method call with connected device names.
  - **Returns**: `Task<int>` - The total count of connected devices.

- **`GetDeviceInfoAsync(string deviceName)`**
  - **Purpose**: Retrieves detailed information about a specific connected device.
  - **Parameter**: 
    - `string deviceName`: The name or identifier of the device.
  - **Returns**: `Task<ConnectedDevice>` - An object containing the device details.

### **ICapture**
The `ICapture` interface manages the fingerprint capture process.

#### **Methods**:

- **`StartCaptureAsync(int Timeout = 10, int MinimumQuality = 60)`**
  - **Purpose**: Starts the fingerprint capture process.
  - **Parameters**:
    - `Timeout`: Timeout in milliseconds. `0` stops capture after detecting a finger.
    - `MinimumQuality`: Quality threshold (1-100).
  - **Returns**: `Task<CaptureViewModel>` - The result of the capture operation.

- **`GetTemplateAsync(TemplateRequest templateRequest)`**
  - **Purpose**: Retrieves a fingerprint template.
  - **Parameter**:
    - `TemplateRequest templateRequest`: Specifies the desired fingerprint template format.
  - **Returns**: `Task<TemplateResponse>` - Contains the fingerprint template details.

## **Model Details**

### **CaptureViewModel**
- `BitmapData (string?)`: Base64-encoded representation of the fingerprint image.
- `ErrorCode (string?)`: Status code (`0` for success).
- `ErrorDescription (string?)`: Description of the error, if any.
- `Quality (int?)`: Quality score (1-100).
- `TemplateData (string?)`: Base64-encoded data of the captured fingerprint template.

### **TemplateRequest**
- `TmpFormat (int)`: Desired fingerprint template format (default is 0).

### **TemplateResponse**
- `ErrorCode (string)`: Status code (`0` for success).
- `ErrorDescription (string)`: Error description if the extraction fails.
- `ImgData (string)`: Base64-encoded image data from the template.

### **IEnroll<TContext>**
The `IEnroll<TContext>` interface handles fingerprint enrollment.

#### **Methods**:

- **`EnrollAsync(EnrollmentRequest enroll)`**
  - **Purpose**: Enrolls fingerprint data into the system.
  - **Parameters**: 
    - `EnrollmentRequest enroll`: Includes fingerprint information and template type.
  - **Returns**: `Task<EnrollmentResponse>` - Outcome of the enrollment operation.

#### **Model Details**
- **EnrollmentRequest**:
  - `FingerPrintData (string?)`: Raw fingerprint data, base64-encoded.
  - `ExternalId (string?)`: Unique identifier, often a new GUID.
  - `TemplateTypeId (string)`: Type of fingerprint template.

- **EnrollmentResponse**:
  - `Code (int?)`: Status code (`0` for success).
  - `Message (string?)`: Additional information.
  - `ErrorMessage (string?)`: Error message, if any.
  - `Template (Byte[])`: Byte array of the enrolled fingerprint template.

### **ISearch<TContext>**
The `ISearch<TContext>` interface handles searching for fingerprint matches.

#### **Methods**:

- **`SearchAsync(SearchRequest search)`**
  - **Purpose**: Searches for fingerprint matches in the database.
  - **Parameters**: 
    - `SearchRequest search`: Contains fingerprint data and search parameters.
  - **Returns**: `Task<SearchResponse>` - The search result, or null if the search fails.

#### **Model Details**
- **SearchRequest**:
  - `FingerPrint (string)`: Base64-encoded fingerprint data.

- **SearchResponse**:
  - `SubjectId (string)`: Unique identifier.
  - `Template (string)`: Data of the matched fingerprint template.
  - `ErrorMessage (string)`: Error message, if any.

### **IMatch**
The `IMatch` interface handles fingerprint matching operations.

#### **Methods**:

- **`MatchAsync(MatchRequest match)`**
  - **Purpose**: Matches two fingerprints.
  - **Parameters**: 
    - `MatchRequest match`: Contains user-provided fingerprint data.
  - **Returns**: `MatchResponse` - Result of the match operation.

- **`VerifyAsync(VerifyRequest verifyRequest)`**
  - **Purpose**: Verifies if two fingerprints match.
  - **Parameters**:
    - `VerifyRequest verifyRequest`: Contains two sets of fingerprint data.
  - **Returns**: `Task<VerifyResponse>` - Verification result.

#### **Model Details**
- **MatchRequest**:
  - `Quality (int)`: Quality score (1-100).
  - `Timeout (int)`: Timeout in milliseconds.
  - `FingerPrintData (string)`: Base64-encoded fingerprint data.
  - `TemplateType (int)`: Type of fingerprint data.

- **MatchResponse**:
  - `BitmapData (string)`: Stored fingerprint data.
  - `ErrorCode (string)`: Status code.
  - `ErrorDescription (string)`: Error description.
  - `Status (bool)`: Match status (`true` or `false`).

## **Using DLL Files and License File**
To enable MxFace functionality:
1. Copy the following files:
   - `MxFace.Fingerprint.SDK.Core.dll`
   - `MxFace.Fingerprint.SDK.Data.dll`
   - `MxFace.Fingerprint.SDK.Extensions.dll`
   - License file: `Fingerprint.lic`
2. Open your project folder.
3. Locate the `bin` folder.
4. Paste the files into the `bin` folder.

### **Code to Enable Services**
Add the following to your project:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    MxFaceEntityFrameworkCoreHelpers.UseMxFaceFingerprintService(modelBuilder);
}

builder.AddMxFaceFingerprintServices();
```

## **Step-by-Step Guide**
### **Device Initialization**
Connect your device, ensure prerequisites are met, and the device will auto-initialize.

### **Capturing Fingerprint**
- Click "Capture 1" to capture.
- View the Status, Quality, and Template Data.

### **Retrieving Template Data**
After capturing, the template data is generated in your specified format.

### **Enrolling Fingerprint**
Ensure the device is initialized and the fingerprint captured. Enrollment saves the template in the database.

### **Searching Fingerprint**
- Ensure prerequisites.
- Use the search feature to retrieve `SubjectId` and `Image Data`.

### **Capture and Match**
- Capture a fingerprint to match against a previously captured one.
- Displays “Matched” or “Unmatched”.

### **Match**
Ensure initialization and capture two fingerprints. Click "Match" to verify.

---

This format should be both clear and comprehensive, helping users understand the functionalities and steps to use the MxFace Fingerprint Software efficiently.
