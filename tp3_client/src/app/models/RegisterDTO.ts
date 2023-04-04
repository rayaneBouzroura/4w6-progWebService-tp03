export class RegisterDTO{
  //constructeur qui suit le dto creer dans le projet dotnet api
  constructor(
    public username: string,
    public email: string,
    public password: string,
    public passwordConfirm: string){}

}
