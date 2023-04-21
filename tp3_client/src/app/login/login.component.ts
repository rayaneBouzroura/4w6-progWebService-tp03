import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { loginDTO } from '../models/loginDTO';
import { UtilisateurService } from 'src/services/utilisateur.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  //create the two double binded variables retrieved from the html form
  loginUsername : string = "";
  loginPassword : string = "";
  constructor(public router : Router , public http : HttpClient , public utilisateurService : UtilisateurService) { }

  ngOnInit() {
  }

  async login() : Promise<void>{
    // //creer un objet login dto avec les données du formulaire
    // const loginDTO : loginDTO = {
    //   username : this.loginUsername,
    //   password : this.loginPassword
    // };
    // //call http  pour login
    // const response = await lastValueFrom(this.http.post<any>('https://localhost:7008/api/Utilisateurs/Login', loginDTO));
    // //print la reponse
    // console.log(response);
    // //save le token dans le local storage
    // localStorage.setItem('token', response.token);
    // // Retourner à la page d'accueil
    // this.router.navigate(['/publicGalleries']);
    try {
      // Call la meth login qui est suppose return une response
      const response = await this.utilisateurService.connection(this.loginUsername, this.loginPassword);

      // Print the response
      console.log(response);

      // Save the token in local storage
      localStorage.setItem('token', response.token);
      //alert the user telling him that il est connecte en tant que "username"
      alert("Vous êtes connecté en tant que " + this.loginUsername);
      // Navigate to the public galleries page
      this.router.navigate(['/publicGalleries']);
    } catch (error: any) {
      console.log(error);
    }

  }

}
