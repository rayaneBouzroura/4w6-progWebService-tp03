import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Galerie } from '../models/galerie';

@Component({
  selector: 'app-publicGalleries',
  templateUrl: './publicGalleries.component.html',
  styleUrls: ['./publicGalleries.component.css']
})
export class PublicGalleriesComponent implements OnInit {


  //liste animal that we will recup du serveur lors du init
  galeries : Galerie[] = [];
  constructor(public http : HttpClient) { }

  ngOnInit() {
    this.getPublicGaleries();
  }

  async getPublicGaleries() : Promise<void>{
    //appelle get oau niveau du endpoint api/Galeries action GetPublicGaleries
    this.galeries = await lastValueFrom(this.http.get<Galerie[]>("https://localhost:7008/api/Galeries/GetAllPublicGaleries"));
    console.log(this.galeries);
    //recup du resultat de la requete

  }

}
