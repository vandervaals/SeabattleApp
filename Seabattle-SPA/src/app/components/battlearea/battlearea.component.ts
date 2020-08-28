import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { fromEvent } from 'rxjs';
import { Area } from 'src/app/_models/area';
import { GameService } from 'src/app/_services/game.service';
import { SignalRService } from 'src/app/_services/signalR.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Shot } from 'src/app/_models/shot';
import { Answer } from 'src/app/_models/answer';

@Component({
  selector: 'app-battlearea',
  templateUrl: './battlearea.component.html',
  styleUrls: ['./battlearea.component.scss']
})
export class BattleareaComponent implements OnInit {
  @ViewChild('yourArea', { static: true })
  yourArea: ElementRef<HTMLCanvasElement>;

  @ViewChild('enemyArea', { static: true })
  enemyArea: ElementRef<HTMLCanvasElement>;

  private yourCtx: CanvasRenderingContext2D;
  private enemyCtx: CanvasRenderingContext2D;
  private pixelToOneStep: number;
  private areaSize: number;
  private area: Area;
  private gameId: number;

  public startGame: boolean;

  constructor(private game: GameService, private signalR: SignalRService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.yourCtx = this.yourArea.nativeElement.getContext('2d');
    this.enemyCtx = this.enemyArea.nativeElement.getContext('2d');
    this.areaSize = this.yourCtx.canvas.width;
    this.pixelToOneStep = this.yourCtx.canvas.width / 10;

    this.drawArea(this.yourCtx);
    this.drawArea(this.enemyCtx);

    this.startGame = false;

    this.area = new Area();
    this.area.drawShips(this.pixelToOneStep, this.yourCtx);
  }

  drawArea(ctx: CanvasRenderingContext2D) {
    ctx.strokeStyle = 'black';
    ctx.lineWidth = 1;

    ctx.beginPath();
    for (let i = this.pixelToOneStep; i <= this.areaSize - this.pixelToOneStep;) {
      ctx.moveTo(i, 0);
      ctx.lineTo(i, ctx.canvas.width);
      ctx.moveTo(0, i);
      ctx.lineTo(ctx.canvas.width, i);
      i += this.pixelToOneStep;
    }
    ctx.closePath();
    ctx.stroke();
  }

  makeShot(event) {
    if (!this.startGame) {
      return;
    }

    const shot: Shot = { x: Math.floor(event.offsetX / this.pixelToOneStep),
                          y: Math.floor(event.offsetY / this.pixelToOneStep) }
    this.game.shot(shot).subscribe((result: Answer) => {
      if (result.inTarget) {
        this.drawCross(shot.x, shot.y, this.enemyCtx);
      } else {
        this.drawCircle(shot.x, shot.y, this.enemyCtx);
        this.startGame = false;
      }

      if (!result.gameContinue) {
        if (result.areYouWinner) {
          this.alertify.success('You win!!!');
        } else {
          this.alertify.success('You lost...');
        }
        this.startGame = false;
      }
    });
  }

  drawCross(x: number, y: number, ctx: CanvasRenderingContext2D) {
    ctx.strokeStyle = 'red';
    ctx.lineWidth = 5;

    ctx.beginPath();
    ctx.moveTo(x * this.pixelToOneStep, y * this.pixelToOneStep);
    ctx.lineTo((x + 1) * this.pixelToOneStep, (y + 1) * this.pixelToOneStep);
    ctx.moveTo((x + 1) * this.pixelToOneStep, y * this.pixelToOneStep);
    ctx.lineTo(x * this.pixelToOneStep, (y + 1) * this.pixelToOneStep);
    ctx.closePath();
    ctx.stroke();
  }

  drawCircle(x: number, y: number, ctx: CanvasRenderingContext2D) {
    ctx.strokeStyle = 'blue';
    ctx.lineWidth = 5;

    ctx.beginPath();
    ctx.arc(Math.round((x + 0.5) * this.pixelToOneStep),
    Math.round((y + 0.5) * this.pixelToOneStep), 5, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.stroke();
  }

  refreshShips() {
    this.yourCtx.clearRect(0, 0, this.areaSize, this.areaSize);
    this.drawArea(this.yourCtx);

    this.area.refreshShipsLocation();
    this.area.drawShips(this.pixelToOneStep, this.yourCtx);
  }

  beginGame() {
    this.game.register(this.area.ships, this.signalR.connsectionId).subscribe((result) => {
      this.gameId = result;
      this.alertify.success('Game start');
    }, error => {
      this.alertify.error('Game register failed');
    });
    this.startGame = true;
  }

}
