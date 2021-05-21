import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';

export class Message {
  content: string;
  author: string;
}

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  backendResponse: string;
  firstName: string;
  lastName: string;

  message: string;
  messages: Array<Message>;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get("https://localhost:44313/" + "account" + "/getCurrentUser").subscribe(response => {  //Dodajemy do parametru po przez ?message=
      this.firstName = (response as any).firstName;
      this.lastName = (response as any).lastName;

      this.refreshMessages();

      var connection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Information)
        .withUrl("https://localhost:44313/message").build()

      connection.start();

      connection.on("NewMessage", () => {
        this.refreshMessages();
      })
    },
      error => { });
  }

  //sendRequestToBackend() // Orzymywanie wiadomości 
  //{
  //  this.http.get("https://localhost:44313/" + "kurs"+"\getMessage?message=").subscribe(response => {
  //      this.backendResponse = response.toString();
  //      },
  //      error => {
  //          this.backendResponse = error;
  //      })
  //}

  //sendRequestToBackend() // wysyłanie krótkich wiadomosci 
  //{
  //  this.http.get("https://localhost:44313/" + "kurs" + "/sendMessage?message=JakasWiadomosc").subscribe(response => {  //Dodajemy do parametru po przez ?message=
  //    this.backendResponse = (response as any).content;
  //  },
  //    error => {
  //      this.backendResponse = error;
  //    })
  //}
  refreshMessages() {
    this.http.get<Array<Message>>("https://localhost:44313/" + "kurs" + "/getMessages").subscribe(response => {  //Dodajemy do parametru po przez ?message=
      this.messages = response;
    },
      error => {
        this.backendResponse = error;
      })
  }

  sendRequestToBackend() // Wysyłanie dłuższysz wiadomosci po przez json 
  {
    var message = new Message();
    message.content = this.message;
    message.author = this.firstName + " " + this.lastName;

    this.http.post<Message>("https://localhost:44313/" + "kurs" + "/sendMessage", message).subscribe(response => {  //Dodajemy do parametru po przez ?message=
      this.backendResponse = response.content;
    },
      error => {
        this.backendResponse = error;
      })
  }

}
