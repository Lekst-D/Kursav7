"""Shop URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/4.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path, re_path
from django.conf import settings
from django.conf.urls.static import static
from hello import views

urlpatterns = [
    path('admin/', admin.site.urls),
    path("", views.index,name="main_page"),
    path("main/", views.main),
    path("login/", views.login_user,name="login"),
    path("game/<str:ID_GET>", views.game),
    path("PageBought/", views.PageBought),
    path("UserInfo/", views.UserInfo,name="UserInfo"),

    path("registration/", views.registration),
    path("swap_password/", views.swap_password),
    
    re_path('exit', views.exit),
    path("UserWallet/", views.UserWallet,name="UserWallet"),
    path("Basket/", views.Basket,name="Basket"),
    path("Basket/<str:ID_GET>/", views.BasketGame,name="BasketGame"),
    path("GameInfoGet",views.GameInfoGet,name="GameInfoGet"),
    path("GameInfoSave/", views.GameInfoSave),
    path("GamePayCheck/", views.GamePayCheck,name="GamePayCheck"),
    path("PayGameUser/", views.PayGameUser,name="PayGameUser"),

    path("testGetZap", views.testGetZap,name="testGetZap"),
    path("testGetZap1", views.testGetZap1,name="testGetZap1"),
    path("GetImage", views.GetImage,name="GetImage"),
    path("login_zap/<str:Log>/<str:Pas>/", views.login_zap),
    
    path("Login_destop", views.Login_destop,name="Login_destop"),
    path("Games_destop", views.Games_destop,name="Games_destop"),
    path("Game_destop", views.Game_destop,name="Game_destop"),
    path("UserGames_destop", views.UserGames_destop,name="UserGames_destop"),
    path("Registration_destop", views.Registration_destop,name="Registration_destop"),
    
]

if settings.DEBUG:
    urlpatterns += static(settings.MEDIA_URL,
                          document_root=settings.MEDIA_ROOT)
    urlpatterns += static(settings.STATIC_URL, document_root=settings.STATIC_ROOT)