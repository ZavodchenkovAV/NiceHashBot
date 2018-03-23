<Query Kind="SQL">
  <Connection>
    <ID>3431c1f6-badd-49c6-a22f-04baaad4ab66</ID>
    <Persist>true</Persist>
    <Provider>System.Data.SqlServerCe.4.0</Provider>
    <AttachFileName>C:\Users\zavodchenkov\source\github\NiceHashBot\src\NiceHashMon\bin\Debug\mon.sdf</AttachFileName>
  </Connection>
</Query>

create Table Coin
( ID bigint not null identity(1,1) primary key,
CoinName nvarchar(50) not null,
Algorithm int not null,
HashRate float not null,
ExplorerUrl nvarchar(100) not null,
BlockTime int not null,
CoinPrize int not null,
CoinPerDay int null,
ActualPrice float null,
ActualPools nvarchar(200) null
)
alter table coin add column profit float null;
--insert into Coin (CoinName,Algorithm,HashRate,ExplorerUrl,BlockTime,CoinPrize,CoinPerDay)
--values('OMEGA',3,73.4155,'https://explorer.omegacoin.network/',60,50,72000)