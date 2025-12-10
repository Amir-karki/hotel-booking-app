# Deployment Guide - Azure App Service

This guide will walk you through deploying the Hotel Booking App to Azure App Service for free.

## Prerequisites

1. **GitHub Account** - Your code should be in a GitHub repository
2. **Azure Account** - Create a free account at [azure.microsoft.com](https://azure.microsoft.com/free/)
3. **Git** - Ensure your code is committed to the repository

## Step-by-Step Deployment

### Step 1: Create Azure App Service

1. Go to [Azure Portal](https://portal.azure.com)
2. Click **"Create a resource"**
3. Search for **"Web App"** and click **Create**

4. Fill in the details:
   - **Subscription**: Select your subscription
   - **Resource Group**: Create new (e.g., `hotel-booking-rg`)
   - **Name**: `hotel-booking-app` (must be globally unique, try `hotel-booking-app-yourname`)
   - **Publish**: Code
   - **Runtime stack**: .NET 8 (LTS)
   - **Operating System**: Linux
   - **Region**: Choose closest to you (e.g., East US)

5. **Pricing Plan**:
   - Click **"Change size"**
   - Select **"Dev/Test"** tab
   - Choose **"F1"** (Free tier - 1 GB RAM, 1 GB storage)
   - Click **Apply**

6. Click **"Review + create"**, then **"Create"**
7. Wait for deployment to complete (1-2 minutes)

### Step 2: Configure App Service for SQLite

1. After creation, go to your App Service
2. In the left menu, click **"Configuration"** (under Settings)
3. Click **"General settings"** tab
4. Set **"SCM Basic Auth Publishing Credentials"** to **On**
5. Click **"Save"** at the top

6. In the left menu, click **"Advanced Tools"** (under Development Tools)
7. Click **"Go"** to open Kudu console
8. In Kudu, click **"Debug console"** > **"CMD"**
9. Navigate to the file system and create a data directory:
   ```
   cd /home
   mkdir data
   ```

### Step 3: Download Publish Profile

1. In Azure Portal, go to your App Service overview page
2. Click **"Get publish profile"** button at the top
3. This downloads a `.PublishSettings` file
4. Open the file in a text editor and copy **all content**

### Step 4: Configure GitHub Secrets

1. Go to your GitHub repository
2. Click **"Settings"** tab
3. In left sidebar, click **"Secrets and variables"** > **"Actions"**
4. Click **"New repository secret"**
5. Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
6. Value: Paste the entire content of the publish profile file
7. Click **"Add secret"**

### Step 5: Update Workflow File (Optional)

The workflow file is already created at [.github/workflows/azure-deploy.yml](.github/workflows/azure-deploy.yml)

If your Azure app name is different from `hotel-booking-app`, edit the file:

```yaml
env:
  AZURE_WEBAPP_NAME: your-actual-app-name  # Change this
```

### Step 6: Deploy

1. Commit all changes:
   ```bash
   git add .
   git commit -m "Add Azure deployment configuration"
   git push origin main
   ```

2. GitHub Actions will automatically start deploying
3. Monitor progress:
   - Go to your GitHub repository
   - Click **"Actions"** tab
   - Watch the deployment workflow

4. Deployment takes 3-5 minutes

### Step 7: Verify Deployment

1. Once deployment succeeds, visit your app:
   ```
   https://your-app-name.azurewebsites.net
   ```

2. The app will automatically:
   - Apply database migrations
   - Seed initial data (hotels, rooms, admin/user accounts)
   - Set up authentication

3. Test login with demo accounts:
   - **Admin**: admin@hotelbooking.com / Admin@123
   - **User**: user@hotelbooking.com / User@123

## Troubleshooting

### App doesn't start

1. Go to Azure Portal > Your App Service
2. Click **"Log stream"** (under Monitoring)
3. Check for errors

### Database issues

1. Ensure `/home/data/` directory exists in Kudu console
2. Check that [appsettings.Production.json](appsettings.Production.json) has correct path
3. Verify migrations ran successfully in deployment logs

### Deployment fails

1. Check GitHub Actions logs for specific errors
2. Verify publish profile secret is correctly set
3. Ensure app name in workflow matches Azure app name

### App sleeps (Free tier limitation)

- Free tier apps sleep after 20 minutes of inactivity
- First request after sleep takes 10-30 seconds
- Upgrade to Basic tier (paid) to prevent sleeping

## Custom Domain (Optional)

1. In Azure Portal, go to your App Service
2. Click **"Custom domains"** (under Settings)
3. Follow instructions to add your domain
4. Free tier supports custom domains

## Environment Variables

To add environment variables:

1. Azure Portal > App Service > **"Configuration"**
2. Click **"New application setting"**
3. Add key-value pairs
4. Click **"Save"**

## Continuous Deployment

Already configured! Every push to `main` branch will automatically deploy to Azure.

To deploy manually:
- Go to GitHub > Actions
- Click **"Deploy to Azure App Service"**
- Click **"Run workflow"**

## Monitoring

1. **Application Insights** (Optional):
   - Enable in Azure Portal for detailed monitoring
   - Free tier: 1 GB data/month

2. **Metrics**:
   - Azure Portal > App Service > **"Metrics"**
   - View CPU, memory, requests, response time

## Upgrading to Paid Tier

When ready to scale:

1. Azure Portal > App Service > **"Scale up (App Service plan)"**
2. Choose **Basic B1** ($13/month):
   - No sleep mode
   - Custom SSL
   - More resources

## Database Backup

SQLite database is stored at `/home/data/hotelbooking.db`

To backup:
1. Kudu console > Debug console > CMD
2. Navigate to `/home/data/`
3. Download `hotelbooking.db` file

## Security Notes

1. **HTTPS**: Automatically enabled by Azure
2. **Secrets**: Never commit publish profiles or secrets to Git
3. **Authentication**: Identity is already configured
4. **File uploads**: Profile pictures stored in `/home/uploads/profiles/`

## Cost Estimate

**Free Tier F1**:
- Cost: $0/month
- Limitations:
  - 60 minutes/day compute time
  - Apps sleep after 20 minutes
  - 1 GB RAM, 1 GB storage
  - Shared infrastructure

Perfect for development, testing, and portfolio projects!

## Support

- Azure Free Tier: 12 months free services
- After 12 months, F1 tier remains free forever
- Monitor usage in Azure Portal > **Cost Management**

## Next Steps

1. ✅ Deploy to Azure
2. Test all features (booking, reviews, admin panel)
3. Share your live app URL
4. Consider adding custom domain
5. Monitor performance and usage

Your app is now live at: `https://your-app-name.azurewebsites.net`
