import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Galerie } from 'src/app/models/galerie';

@Injectable({
  providedIn: 'root'
})
export class GalerieService {

constructor(public http : HttpClient) { }
  //creer galerie service
  async creerGalerie(nomGalerie: string, estPublique: boolean): Promise<void> {
    let galerieCreer: Galerie = new Galerie(0, "", nomGalerie, estPublique);
    await this.http.post("https://localhost:7008/api/Galeries/PostGalerie", galerieCreer).toPromise();
  }
  //recup liste galerie service
  // async retrieveUserGaleries() : Promise<Galerie[]>{
  //   if (localStorage.getItem('token') !== null) {
  //     let galeries = await lastValueFrom(this.http.get<Galerie[]>("https://localhost:7008/api/Galeries/GetUserGaleries"));;
  //     //si galerie pas undefined ont la retourne
  //     if (galeries !== undefined) {
  //       return galeries;
  //     }

  //   } else {
  //     throw new Error('pas de token dans le local storage donc no user is connected ');
  //   }
  // }
  //update galerie visibility
  async updateGalerieVisibility(galerie: Galerie, choix: boolean): Promise<void> {
    galerie.estPublique = choix;
    const url = "https://localhost:7008/api/Galeries/PutGalerie/" + galerie.id;
    let response = await  lastValueFrom(this.http.put(url, galerie));

  }
  async shareWithUser(galerie: Galerie, nomPersonne: string): Promise<void> {
    const url = "https://localhost:7008/api/Galeries/PartagerGalerie/"+galerie.id+"/"+nomPersonne;
    let response = await  lastValueFrom(this.http.put(url, galerie));
    var v = await lastValueFrom(this.http.put(url, {}));//body empty mais necessaire fvu que c put

  }
  async delete(galerieId: number | undefined ): Promise<void> {
    await lastValueFrom(this.http.delete("https://localhost:7008/api/Galeries/DeleteGalerie/" + galerieId));
  }


}


