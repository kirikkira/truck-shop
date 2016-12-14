//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
FILE *Fz;
AnsiString File_Zap;
int u=0;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
int b=0,g=0,n=0;
randomize();
 try
{
  StrToInt(Edit1->Text);
}
catch(...)
{
  ShowMessage("Некорректный ввод. Пожалуйста повторите ввод данных.");
   goto gr;
  }
  b=StrToInt(Edit1->Text);
if (b>551) ShowMessage("Некоректный ввод.Превышено значение размера поля.");else
{
    if (u==0)
  {
  u++;

for (int i=1; i<=b;i++)
{
while ((n==0)||(g==0)||(StringGrid1->Cells[n][g]=="O"))
{
n=random(29);
g=random(19);
}
StringGrid1->Cells[n][g]="O";
n=0;
g=0;
}
Button2->Visible=true;
Button1->Visible=false;
gr:
Edit1->Text="";
} else{
Button1->Visible=false;}}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::StringGrid1DrawCell(TObject *Sender, int ACol,
      int ARow, TRect &Rect, TGridDrawState State)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button2Click(TObject *Sender)
{
for (int i=1;i<=29;i++)
{
for (int j=1;j<=19;j++)
{
if (StringGrid1->Cells[i][j]=="X") StringGrid1->Cells[i][j]="";
if (StringGrid1->Cells[i][j]=="o") StringGrid1->Cells[i][j]="O";
}}
int k=0,f=0,v=0;
for (int i=1;i<=29;i++)
{
for (int j=1;j<=18;j++)
{
if (StringGrid1->Cells[i][j]=="O")
{
if (StringGrid1->Cells[i-1][j-1]=="O") k++;
if (StringGrid1->Cells[i][j-1]=="O") k++;
if (StringGrid1->Cells[i+1][j-1]=="O") k++;
if (StringGrid1->Cells[i+1][j]=="O") k++;
if (StringGrid1->Cells[i-1][j]=="O") k++;
if (StringGrid1->Cells[i-1][j+1]=="O") k++;
if (StringGrid1->Cells[i][j+1]=="O") k++;
if (StringGrid1->Cells[i+1][j+1]=="O") k++;
if ((k>3)||(k<2))  {StringGrid1->Cells[i][j]="X";}
if ((k==2)||(k==3)) {v++;}
}
else
{
if (StringGrid1->Cells[i-1][j-1]=="O") f++;
if (StringGrid1->Cells[i][j-1]=="O") f++;
if (StringGrid1->Cells[i+1][j-1]=="O") f++;
if (StringGrid1->Cells[i+1][j]=="O") f++;
if (StringGrid1->Cells[i-1][j]=="O") f++;
if (StringGrid1->Cells[i-1][j+1]=="O") f++;
if (StringGrid1->Cells[i][j+1]=="O") f++;
if (StringGrid1->Cells[i+1][j+1]=="O") f++;
if (f==3) {StringGrid1->Cells[i][j]="o";}
if (f!=3) {v++;}
}
k=0;f=0;
}
}
if (v==551)     {ShowMessage("Конец игры");Button2->Visible=false;v=0;}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button3Click(TObject *Sender)
{
u=0;
for (int i=1;i<=29;i++)
{
for (int j=1;j<=19;j++)
{
StringGrid1->Cells[i][j]="";
}}
Edit1->Text="";
Button1->Visible=true;
Button2->Visible=false;
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button5Click(TObject *Sender)
{
Form1->Close();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button4Click(TObject *Sender)
{
Form2->Show();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button7Click(TObject *Sender)
{
OpenDialog1->FileName="данные.txt";
OpenDialog1->Title="Открыть файл базы данных";
        if (OpenDialog1->Execute())
         {
                TStringList *tsl=new TStringList;
                tsl->LoadFromFile(File_Zap = OpenDialog1->FileName);
                if ((Fz=fopen(File_Zap.c_str(),"rb"))==NULL)
                 {
                        ShowMessage("Ошибка открытия ФАЙЛА!");
                        return;
                 }
                          Edit1->Text=tsl->Strings[0];
                       delete tsl;
        fclose(Fz);
         }
}
void __fastcall TForm1::Button6Click(TObject *Sender)
{ int w=0;
randomize();
while (w==0)
w=random(551);
Edit1->Text=IntToStr(w);
