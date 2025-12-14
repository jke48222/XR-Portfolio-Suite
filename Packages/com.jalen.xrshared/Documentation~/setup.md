# Setup

1) Add local package reference in each app:
"com.jalen.xrshared": "file:../../Packages/com.jalen.xrshared"

2) Create an AppConfig asset:
Assets/_App/Config/AppConfig.asset

3) Create a Bootstrap scene:
- Add empty GameObject named AppRoot
- Add AppBootstrap component
- Assign AppConfig asset

4) Add scenes to Build Settings:
- Bootstrap
- Experience
