# Generated by Django 4.1.3 on 2022-12-01 06:27

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('hello', '0004_usergalary_wallet_walletmoney_delete_user'),
    ]

    operations = [
        migrations.AddField(
            model_name='usergalary',
            name='ID_user',
            field=models.ForeignKey(default=11, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL),
            preserve_default=False,
        ),
        migrations.CreateModel(
            name='UserListBuy',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('Status', models.CharField(max_length=15)),
                ('DateOfBuy', models.CharField(max_length=10)),
                ('ID_Game', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='hello.game')),
                ('ID_user', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
    ]