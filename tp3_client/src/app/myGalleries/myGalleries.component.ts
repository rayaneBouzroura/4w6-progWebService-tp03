import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Galerie } from '../models/galerie';

@Component({
  selector: 'app-myGalleries',
  templateUrl: './myGalleries.component.html',
  styleUrls: ['./myGalleries.component.css']
})
export class MyGalleriesComponent implements OnInit {

  constructor(public http : HttpClient) { }
  //liste de galerie
  galeries : Galerie[] = [];
  //la gallerie selectionner en double binding donc peut etre null via ts
  galerie : Galerie | null = null;

  //double binding pour le nom et si galerie pub ou priv
  //string et bool peuvent etre null via ts
  nomGalerie : string | null = null;
  estPublique : boolean  = false;
  ngOnInit() {
    this.recupListeGaleriesCurrentUser();
  }
  async recupListeGaleriesCurrentUser() :Promise<void>{
    //appelle get oau niveau du endpoint api/Galeries action GetMyGaleries
    this.galeries = await lastValueFrom(this.http.get<Galerie[]>("https://localhost:7008/api/Galeries/GetUserGaleries"));
    console.log(this.galeries);

  }
  async deleteCurrentGalerie() : Promise<void>{
    //appelle delete oau niveau du endpoint api/Galeries action DeleteGalerie
    await lastValueFrom(this.http.delete("https://localhost:7008/api/Galeries/DeleteGalerie/" + this.galerie?.id));
    this.recupListeGaleriesCurrentUser();
  }
  async creerGalerie () : Promise<void>{
    //creer une galerie via double binding
    //check que rien n'est null
    if(this.estPublique == null) console.log("estPublique null");
    if (this.nomGalerie != null && this.estPublique != null){
      //creation de la galerie
      let galerieCreer : Galerie = new Galerie(0,"",this.nomGalerie,this.estPublique);
      //appelle post oau niveau du endpoint api/Galeries action CreateGalerie
      let x = await lastValueFrom(this.http.post("https://localhost:7008/api/Galeries/PostGalerie",galerieCreer));
      console.log(x);
      this.recupListeGaleriesCurrentUser();
    }
    else{
      console.log("erreur");
    }
    }
  selectGalerie(galSelectionner:Galerie){
    //ref vers la gallerie selectionner
    this.galerie = galSelectionner;
    console.log(this.galerie);
  }

}
