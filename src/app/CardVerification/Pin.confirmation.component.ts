import { Component, Injectable, Renderer2, ElementRef, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Component({
    selector: "pm-pin",
    templateUrl: "./pinconfirmation.component.html"
})
    @Injectable()
export class PinComponent implements OnInit
{
   
    constructor(private http:HttpClient,private renderer:Renderer2, private element: ElementRef) {
       
        
    }
    ngOnInit(): void {
       this.HidePinForm()
   
    }
    pin: string
    cvv: string
    HidePinForm() {
        var val =  this.element.nativeElement.querySelector(".pin-element")
        this.renderer.addClass(val,"d-none")
}
    OnVerify = (pin:string,cvv:string) => {
        console.log(pin)
        
}
}