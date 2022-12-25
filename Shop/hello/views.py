from django.shortcuts import render
from django.http import HttpRequest 

from .models import Game,UserListBuy,Wallet,WalletMoney,UserGalary

from django.http import JsonResponse
from django.contrib.auth.models import User
from django.contrib.auth import authenticate, login, logout
from django.shortcuts import redirect


def index(request: HttpRequest):
    games= Game.objects.all()
    us=request.user
    data = {"Games": games,"user":us}
    return render(request, "index.html",data)

def login_user(request):
    if request.method == "POST":
        Log = request.POST.get("Login")
        Pas = request.POST.get("Password")
        user = authenticate(request, username=Log, password=Pas)
        if user is not None:
            login(request, user)
            data = {"Nic": "000"}
            return redirect(index)
        else:    
            data = {"Nic": "111"}
            return render(request, "login.html",data)
    else:
        data = {"Nic": "as22"}
        return render(request, "login.html",data)

def game(request,ID_GET):
    game=Game.objects.get(id=ID_GET)
    data = {"Game_ID":ID_GET,"Game_name": game.Name,"Creater":game.Creater,"DateEnter": game.DateCreate,"GamePrice":game.Price,"Description": game.Description,"img":game.cover}
    return render(request, "game.html",data)

def PageBought(request):
    data = {"Game_name": "Game_name","DateEnter": "DateEnter","Creater": "Creater","Description": "Description"}
    return render(request, "PageBought.html",data)

def GameInfoGet(request):
    if request.method == "POST":
        id_game=request.POST.get("GameID")
        game=Game.objects.get(id=id_game)
        return JsonResponse({"IDGame": game.id,"GamePrice":game.Price[0:-5],"ImageGame": game.cover.url, "NameGame": game.Name,"DateCreateGame": game.DateCreate,"CreaterGame":game.Creater,"DescriptionGame":game.Description})
    else:
        return JsonResponse({"IDGame":"","ImageGame": "", "NameGame": "","DateCreateGame":"","CreaterGame":"","DescriptionGame":""})

def GamePayCheck(request):
    if request.method == "POST":
        Money=float(request.POST.get('Money'))
        us=(request.user.id)
        wal=Wallet.objects.get(ID_user_id=us)
        walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)
        monwal=float(walMoney.Money)
        if(monwal>=Money):
            return JsonResponse({'check':"Yes"})
        else:
            return JsonResponse({'check':"No"})
        
def PayGameUser(request):
    if request.method == "POST":
        IDGame=int(request.POST.get('ID_Game'))
       
        us=int(request.user.id)

        game=Game.objects.get(id=IDGame)
        wal=Wallet.objects.get(ID_user_id=us)
        walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)
        userBuys=UserListBuy.objects.filter(ID_user_id=us,ID_Game_id=game.id)[0]
       

        monwal=float(walMoney.Money)
        walMoney.Money=monwal-float(game.Price[0:-5])
        walMoney.save()

        userBuys.Status="Оплачено"
        userBuys.save()
        

        userGal=UserGalary.objects.create(ID_Game_id=game.id, ID_user_id=us)

        return JsonResponse({'check':"Yes"})

def Basket (request):
    us=(request.user.id)
    userBuys=UserListBuy.objects.filter(ID_user_id=us,Status="Неоплачено")
    games=[]
    for c in userBuys:
        
        games.append(Game.objects.get(id =c.ID_Game_id ))

    data = {"UserID": us,"games":games}
    return render(request, "basket.html",data)

def BasketGame (request,ID_GET):
    if request.method == "POST":
        #Name=request.POST.get("GamePrice")
        #UserListBuy.objects.all().delete()
        us=(request.user.id)
        userHaveBuy=UserListBuy.objects.filter(ID_user_id=us , ID_Game_id=ID_GET)
        if(not userHaveBuy):
            userList=UserListBuy.objects.create(ID_user_id=us , ID_Game_id=ID_GET,Status="Неоплачено")

        userBuys=UserListBuy.objects.filter(ID_user_id=us,Status="Неоплачено")
        games=[]
        for c in userBuys:
            
            games.append(Game.objects.get(id =c.ID_Game_id ))

        data = {"UserID": us,"games":games}
        return render(request, "basket.html",data)

def UserInfo(request:HttpRequest):
    us=int(request.user.id)
    wal=Wallet.objects.get(ID_user_id=us)
    walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)

    userGal=UserGalary.objects.filter( ID_user_id=us)
    games=[]
    for c in userGal:
        game=Game.objects.get(id=c.ID_Game_id)
        games.append(game)

    data = {"Wallet": walMoney.Money,"Games":games}
    return render(request, "UserInfo.html",data)

def UserWallet(request:HttpRequest):
    if request.method == "POST":
        Money1 = float(request.POST.get("Money"))
        us=int(request.POST.get("userID"))
        wal=Wallet.objects.get(ID_user_id=us)
        walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)
        monwal=float(walMoney.Money)
        walMoney.Money=monwal+Money1
        walMoney.save()

        return JsonResponse({'foo':walMoney.Money})
    return JsonResponse({'foo':"111"})

def login_zap(request,Log,Pas):
    game=Game.objects.get(id=8)
    return JsonResponse({"name": game.Name, "age": Pas})

from django.views.decorators.csrf import csrf_exempt
@csrf_exempt
def testGetZap(request):
    if request.method == "POST":
        getzap=int(request.POST.get("id"))
        #getzap=int(request.GET['id'])
        game=Game.objects.get(id=getzap)
        return JsonResponse({"name": game.Name})
    else:
        getzap=int(request.GET['id'])
        game=Game.objects.get(id=getzap)
        return JsonResponse({"name": game.Name})

@csrf_exempt
def testGetZap1(request):
    if request.method == "POST":
        getzap=int(request.POST.get("id"))
        #getzap=int(request.GET['id'])
        game=Game.objects.get(id=getzap)
        return JsonResponse({"name": game.Name})

import os
import base64
@csrf_exempt
def GetImage(request):
    game=Game.objects.get(id=8)
    file = open(game.cover.path)
    result = file.read()
    result = base64.b64encode(result)
    return HttpResponse(result)



@csrf_exempt
def Login_destop(request):
    if request.method == "POST":
        Log = request.POST.get("Login")
        Pas = request.POST.get("Password")
        user = authenticate(request, username=Log, password=Pas)
        wal=Wallet.objects.get(ID_user_id=user.id)
        walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)
        return JsonResponse({"id": user.id,"username":user.username,"email":user.email,"wallet":walMoney.Money})
    else:
        us=User.objects.get(id=11)

        wal=Wallet.objects.get(ID_user_id=us.id)
        walMoney=WalletMoney.objects.get(ID_Wallet_id=wal.id)
        return JsonResponse({"id": us.id,"username":us.username,"email":us.email})
        #return JsonResponse({"id": user.id,"username":user.username,"email":user.mail,"wallet":walMoney.Money})

@csrf_exempt
def UserGames_destop(request):
    if request.method == "POST":
        us = int(request.POST.get("id_user"))
        list_game=[]
        userGal=UserGalary.objects.filter( ID_user_id=us)
        games=[]
        for c in userGal:
            game=Game.objects.get(id=c.ID_Game_id)
            games.append(game)
        for c in games:
            list_game.append({"id": c.id,"name": c.Name,"cover":c.cover.url,"date": c.DateCreate,"price": c.Price})
        return JsonResponse(list_game,safe=False)  
    else:
        us = 11
        list_game=[]
        userGal=UserGalary.objects.filter( ID_user_id=us)
        games=[]
        for c in userGal:
            game=Game.objects.get(id=c.ID_Game_id)
            games.append(game)
        for c in games:
            list_game.append({"id": c.id,"name": c.Name,"cover":c.cover.url,"date": c.DateCreate,"price": c.Price})
        return JsonResponse(list_game,safe=False)  

def Games_destop(request):
    game=(Game.objects.all())
    list_game=[]
    #user=User.objects.all()[0]
    #return JsonResponse({"id": game[0].id,"name": game[0].Name,"cover": game[0].cover,"date": game[0].DateCreate,"price": game[0].Price})
    for c in game:
        list_game.append({"id": c.id,"name": c.Name,"cover":c.cover.url,"date": c.DateCreate,"price": c.Price})
    #return JsonResponse({"id": game[0].id,"name": game[0].Name,"cover": game[0].cover.url,"date": game[0].DateCreate,"price": game[0].Price})  
    return JsonResponse(list_game,safe=False)  

@csrf_exempt
def Game_destop(request):
    if request.method == "POST":
        ID_game = request.POST.get("id_game")
        game=Game.objects.get(id=ID_game)
        return JsonResponse({"id": game.id,"creater": game.Creater,"name": game.Name,"description": game.Description,"cover": game.cover.url,"date": game.DateCreate,"price": game.Price})  

@csrf_exempt
def Registration_destop(request):
    if request.method == "POST":
        Nic = request.POST.get("Nic")
        Mai = request.POST.get("Mail")
        Pas = request.POST.get("Password")

        user_exit=User.objects.get(username=Nic)
        if not(user_exit):
            user = User.objects.create_user(Nic, Mai, Pas)

            wal=Wallet.objects.create(ID_user=user)
            walCash=WalletMoney.objects.create(ID_Wallet=wal,Valuta="руб.",Money=00.00)
       # return JsonResponse({"id": user.id,"username":user.username,"email":user.email,"wallet":walMoney.Money})

            return JsonResponse({"id":"Yes"})
            return JsonResponse({"id": "Yes","username":"Yes","email":"Yes","wallet":"Yes"})
        else:
            return JsonResponse({"id":"No"})
            return JsonResponse({"id": "No","username":"No","email":"No","wallet":"No"})

def main(request):
    return render(request, "login.html")

def exit(request):
    logout(request)
    games= Game.objects.all()
    data = {"Games": games}
    return render(request, "index.html",data)

def registration(request):
    if request.method == "POST":
        Nic = request.POST.get("Nic")
        Mai = request.POST.get("Mail")
        Pas = request.POST.get("Password")
        user = User.objects.create_user(Nic, Mai, Pas)

        wal=Wallet.objects.create(ID_user=user)
        walCash=WalletMoney.objects.create(ID_Wallet=wal,Valuta="руб.",Money=00.00)

        '''
        walCash=WalletMoney.objects()
        walCash.ID_Wallet=wal.ID
        walCash.Valuta="РУБ"
        walCash.Money="00.00"
        walCash.save()
        '''

        #data = {"Nic": user.id}
        #return render(request, "registration.html",data)
        return redirect(login_user)
    else:
        data = {"Nic": "0000"}
        return render(request, "registration.html",data)

def swap_password(request):
    if request.method == "POST":
        Log = request.POST.get("Login")
        Pas = request.POST.get("Password")
        user = User.objects.filter( Mail=Log ) 
        data = {"Nic": user.Nick}
        return render(request, "login.html",data)
    else:
        data = {"Nic": "as"}
        return render(request, "login.html",data)


def GameInfoSave(request):
    if request.method == 'POST':
        form = Game()
        Name=request.POST.get("Name")
        DateCreate=request.POST.get("DateCreate")
        Price=request.POST.get("Price")
        Description=request.POST.get("Description")
        Creater=request.POST.get("Creater")
        file=request.FILES.get("file")

        form.Name=Name
        form.DateCreate=DateCreate
        form.Price=Price
        form.Description=Description
        form.cover=file
        form.Creater=Creater
        form.save()

        im = Game.objects.all()
        data={"game":im}
        return render(request, "GameInfoSave.html",data)

    else:

        im = Game.objects.all()
        data={"game":im}
        return render(request, "GameInfoSave.html",data)