![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2025

## Team name
Null Terminators
 
## Category
Sitecore Hackathon 2025 - Single Category
 
## Description
AI-Powered Alt Text Generator
 
## Module Purpose:
Alt text is crucial for images in web accessibility and SEO, but manually adding it to images in Sitecore's Media Library can be time-consuming. Our Sitecore Hackathon project introduces a custom button in the Sitecore ribbon (under the Configure tab) that automatically generates and updates the alt text for images using OpenAI's AI capabilities. This ensures better accessibility and improved search engine rankings.
 
## How does this module solve it:
We have developed a custom Sitecore module that:
1. Adds a button in the Sitecore ribbon to trigger alt text updates.
2. Processes the selected item and it's child items.
3. Uses OpenAI API to analyze images and generate meaningful alt text.
4. Updates the Alt field of images where alt text is missing.
 
## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_
 
⟹ [Replace this Video link](#video-link)
 
## Pre-requisites and Dependencies
- SPE Module Installation for Sitecore 10.X  
- Sitecore Package Installation for Custom Button Implementation
 
## Installation instructions  
Follow the steps below to install and set up the module:  
 
1. **Build the Solution**  
- Switch to the `feature/SitecoreHackathon` branch.  
- Build the solution to generate the required DLL files.  
 
2. **Deploy DLLs**  
- Copy `SitecoreHackathon.Feature.UpdateAltTags.dll` and `SitecoreHackathon.Foundation.OpenAI.dll` to the `/bin` folder of your Sitecore instance.  
 
3. **Deploy Configuration Files**  
- Place the following configuration files in the `App_Config\Include\zzz` folder:  
- `SitecoreHackathon.Feature.UpdateAltTags.config`  
- `Sitecore.Foundation.OpenAI.config`  
 
4. **Install the Sitecore Package**  
- Use the **Sitecore Installation Wizard** to install the provided package.  
Package Path - https://github.com/Sitecore-Hackathon/2025-Null-Terminators/blob/feature/SitecoreHackathon/SitecoreHackathon_NullTerminators.zip
- This package will create a **custom button** with name **Alt Tags Update** under the **Configure** tab in the Sitecore ribbon, allowing users to update alt text for media items.  
 
5. **Verify Installed Items**  
- Ensure the following items are installed as part of the package:  
 /sitecore/templates/Project/SitecoreHackathon (contains subitems)
 /sitecore/system/Settings/Services/SitecoreHackathon (contains subitems)
 /sitecore/system/Modules/PowerShell/Script Library/Sitecore Hackathon (contains subitems)
 
6. **Sync PowerShell Library**  
- Click on the **Sitecore icon** (bottom-left of the screen).
![image](https://github.com/user-attachments/assets/50cb0ba1-f848-4127-ae6e-c2a35511ffad)
- Navigate to **Development Tools > PowerShell ISE**. 
![image](https://github.com/user-attachments/assets/2d5814dc-af67-4829-8875-35391367c040)
- In **Settings**, click on **Rebuild All**, then select **Sync Library with Content Editor Ribbon** to add the button to the Sitecore ribbon.
![image](https://github.com/user-attachments/assets/c3a32acb-11f6-4a5f-85d5-af4484d24b8d)

Once these steps are completed, your module will be fully installed and ready to use.
 
## Usage instructions 
Once the module is installed, follow these steps to use it effectively:  
 
### 1. **PowerShell Integration**  

- Click on the **Sitecore icon** (bottom-left of the screen).
![image](https://github.com/user-attachments/assets/ba67c0ce-a550-4430-a06c-e4fbf84ad4ab)
- Navigate to **Development Tools > PowerShell ISE**. 
![image](https://github.com/user-attachments/assets/ce55c945-95d6-4738-97af-831c8327aa7d)
- In the PowerShell ISE, the module's script is available for execution.
- If needed, go to **Settings > Rebuild All > Sync Library with Content Editor Ribbon** to ensure the button appears in the ribbon.
![image](https://github.com/user-attachments/assets/80027cbf-5e85-4dfe-95ac-772b6daf9c72)
 
### 2. **Accessing the Module**  

- Open the **Sitecore Content Editor**.  
- Navigate to the **Configure** tab in the Sitecore ribbon.  
- Locate the **"Alt Tags Update"** button, which was installed as part of the package. 
![image](https://github.com/user-attachments/assets/6b565b60-1133-4896-a6da-26220e2a9f12)

### 3. **Updating Alt Text for Media Items**  

- Select any media item (e.g., images) in the **Media Library**.  
- Click the **"Alt Tags Update"** button in the ribbon.  
- The module will use **OpenAI API** to generate and update meaningful alt text based on the image content.
![image](https://github.com/user-attachments/assets/88e852cc-e591-45a9-b660-b3e31e23a98c)
- If an item does not contain the Alt text field or if it already has text present, a prompt will appear stating: **"No Action Needed. Alt text is either already present or the field does not exist."**
![image](https://github.com/user-attachments/assets/77590483-489f-416c-afe2-03955086dc8f)
 
With these steps, users can easily access the module to enhance accessibility and SEO by automatically generating meaningful alt text for media items.
 
![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
![image](https://github.com/user-attachments/assets/7fb3c0a1-7537-4c16-9359-7e25e33d866f)
 
## Comments
If you'd like to make additional comments that is important for your module entry.
