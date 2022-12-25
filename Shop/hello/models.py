from django.db import models
from django.contrib.auth.models import User

class Game(models.Model):
    Name = models.CharField(max_length=45)
    DateCreate =  models.CharField(max_length=20)
    Price= models.CharField(max_length=20)
    Creater= models.CharField(max_length=45)
    Description= models.CharField(max_length=450)
    cover = models.ImageField(upload_to='images/')

class Wallet(models.Model):
    ID_user=models.ForeignKey(User,on_delete=models.CASCADE)

class WalletMoney(models.Model):
    ID_Wallet=models.ForeignKey(Wallet,on_delete=models.CASCADE)
    Valuta= models.CharField(max_length=45)
    Money= models.DecimalField(decimal_places=2, max_digits=20)

class UserGalary(models.Model):
    ID_user=models.ForeignKey(User,on_delete=models.CASCADE)
    ID_Game=models.ForeignKey(Game,on_delete=models.CASCADE)

class UserListBuy(models.Model):
    ID_user=models.ForeignKey(User,on_delete=models.CASCADE)
    ID_Game=models.ForeignKey(Game,on_delete=models.CASCADE)
    Status= models.CharField(max_length=15)
    DateOfBuy=models.CharField(max_length=10)

