import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { loginDTO } from '../models/loginDTO';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //create the two double binded variables retrieved from the html form
  loginUsername : string = "";
  loginPassword : string = "";
  constructor(public router : Router , public http : HttpClient) { }

  ngOnInit() {
  }

  async login() : Promise<void>{
    //creer un objet login dto avec les données du formulaire
    const loginDTO : loginDTO = {
      username : this.loginUsername,
      password : this.loginPassword
    };
    //call http  pour login
    const response = await lastValueFrom(this.http.post<any>('https://localhost:7008/api/Utilisateurs/Login', loginDTO));
    //print la reponse
    console.log(response);
    //save le token dans le local storage
    localStorage.setItem('token', response.token);
    // Retourner à la page d'accueil
    this.router.navigate(['/publicGalleries']);
  }

}
