import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { RegisterDTO } from '../models/RegisterDTO';
import { UtilisateurService } from 'src/services/utilisateur.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public router : Router,public http : HttpClient , public utilisateurService : UtilisateurService) { }
  //init des variables double binding pour creer le dto suivant le html
  registerUsername: string = "";
  registerEmail: string = "";
  registerPassword: string = "";
  registerPasswordConfirm: string = "";
  ngOnInit() {
  }

  async register() : Promise<void>{
    try {
      await this.utilisateurService.enregistrerUtilisateur(
        this.registerUsername,
        this.registerEmail,
        this.registerPassword,
        this.registerPasswordConfirm
      );
      // Aller vers la page de connexion
      this.router.navigate(['/login']);
    } catch (error : any) {
      console.log(error);
    }

}
}
