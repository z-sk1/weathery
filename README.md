# 🌤️ weathery


weathery is a weather app made by me!!
like my last project, shrtnd, a URL shortener, this one also has both its frontend and backend deployed on Render — a service that lets you host static sites and web services (for free!!).
i could've used AWS or something but eh

## 🔗 live site
👉 weathery-5ijy.onrender.com
yeah, there's a weird code in the URL — it's a free render domain and i can't be bothered to set up a custom one (or buy one haha), so this is what we’re working with.

## ⚙️ backend endpoint
just in case you're curious, the backend lives at:
weathery-service.onrender.com
you probably don’t need it sooooooo

## 🖥️ desktop client (Windows)
### to get the windows version of weathery:

head over to the Releases section

download the .zip or .exe file

run the app — no install needed

### if you want to build it yourself:

open the .sln file in Visual Studio 2022

set the build config to Release and Any CPU

hit build and find the .exe inside bin/Release

## Running the Backend Locally
Since the backend lives online there will be a big delay if you use the app a long time after the last time you used the app. This is because im using a free tier Render account, and to save costs the backend will "sleep" and will take a minute to boot back up. After that it should start responding like normal. If you want to mitigate this and are comfortable, you can download the special zip package of weathery, which also comes with the backend for you to run locally. This will allow you to get faster responses and you won't have to wait for the backend to boot back up after a while.

### Instructions to run the Backend Locally
1. Run **`WeatheryClient.exe`** as normal.
2. Next, run **`WeatheryBackend.exe`**.
3. You're done! You can also click on hidden icons on the task bar and find the weathery server icon and show and hide the console. The console will show you debug info and other stuff if you wanted to take a look.

## Contribution
feel free to peek at the C# code and tinker and send a PR too if you're into that sort of thing.

