import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Galerie } from '../models/galerie';
import { GalerieService } from 'src/services/galerie.service';

@Component({
  selector: 'app-myGalleries',
  templateUrl: './myGalleries.component.html',
  styleUrls: ['./myGalleries.component.css']
})
export class MyGalleriesComponent implements OnInit {

  constructor(public http : HttpClient,public galerieService : GalerieService) { }
  //liste de galerie
  galeries : Galerie[] = [];
  //la gallerie selectionner en double binding donc peut etre null via ts
  galerie : Galerie | null = null;
  //le pseudo de la  personne avec qui ont partage la galerie
  personne : string | null = null;
  //double binding pour le nom et si galerie pub ou priv
  //string et bool peuvent etre null via ts
  nomGalerie : string | null = null;
  estPublique : boolean  = false;
  ngOnInit() {
    this.recupListeGaleriesCurrentUser();
  }
  async recupListeGaleriesCurrentUser() :Promise<void>{
    //appelle get au niveau du endpoint api/Galeries action GetMyGaleries
    this.galeries = await lastValueFrom(this.http.get<Galerie[]>("https://localhost:7008/api/Galeries/GetUserGaleries"));
    console.log(this.galeries);

  }
  async deleteCurrentGalerie() : Promise<void>{
    //appelle delete oau niveau du endpoint api/Galeries action DeleteGalerie
    await this.galerieService.delete(this.galerie?.id);
    this.recupListeGaleriesCurrentUser();
  }


  async creerGalerie () : Promise<void>{
    //check si doublebound data non null
    if (this.nomGalerie != null && this.estPublique != null) {
      try {
        //call service
        await this.galerieService.creerGalerie(this.nomGalerie, this.estPublique);
        this.recupListeGaleriesCurrentUser();
        alert("galerie :" + this.nomGalerie + " a etait creer");
      } catch (error) {
        console.error('Erreur lors de la création de la galerie', error);
        alert("Une erreur s'est produite lors de la création de la galerie");
      }
    } else {
      console.log("erreur");
    }
    }
  selectGalerie(galSelectionner:Galerie){
    //ref vers la gallerie selectionner
    this.galerie = galSelectionner;
    console.log(this.galerie);

  }

  async updateGalerieVisibility(choix : boolean) : Promise<void>{
    if(this.galerie != null){
      //try catch avec le service
      try {
        await this.galerieService.updateGalerieVisibility(this.galerie,choix);
        this.recupListeGaleriesCurrentUser();
        alert("galerie :" + this.galerie.nom + " a etait modifier");
      }catch (error) {
        console.error('Erreur lors de la modification de la galerie', error);
        alert("Une erreur s'est produite lors de la modification de la galerie");
      }
    }
  }

  async partagerGalerie(): Promise<void> {
    // Check if gal et personne not null
    if (this.galerie != null && this.personne != null) {
      // Prepare le endpoint
      // const url = "https://localhost:7008/api/Galeries/PartagerGalerie/"+this.galerie.id+"/"+this.personne;
      // console.log(url);
      // call api via service
      try {
        //appelle au service
        await this.galerieService.shareWithUser(this.galerie, this.personne);
        //var v = await lastValueFrom(this.http.put(url, {}));
        alert("la galerie "+this.galerie.nom + " a etait partager avec "+this.personne);
        this.recupListeGaleriesCurrentUser();
      } catch (error) {
        console.error('Erreur lors du partage de la galerie', error);
        alert("Une erreur s'est produite lors du partage de la galerie :"+this.galerie.nom +"avec"+ this.personne);
      }
    } else {
      console.error('La galerie ou la personne est null');
      alert("Veuillez sélectionner une galerie et entrer le nom d'utilisateur avec qui partager");
    }
  }


}
