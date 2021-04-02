import Calendar, { ISchedule } from 'tui-calendar';
import "tui-calendar/dist/tui-calendar.css";
import "bootstrap/dist/css/bootstrap.min.css"

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';

var cal = new Calendar('#calendar', {
    useCreationPopup: true,
    defaultView: 'week', // set 'week' or 'day'
    taskView: true,  // e.g. true, false, or ['task', 'milestone'])
    scheduleView: ['time']  // e.g. true, false, or ['allday', 'time'])
});

cal.createSchedules([
    {
       // id: '1',
    //    calendarId: '1',
        title: 'my schedule',
        category: 'time',
  //      dueDateClass: '',
        start: '2021-04-02T22:30:00+09:00',
        end: '2021-04-03T22:30:00+09:00',
    },
    {
     //   id: '2',
   //     calendarId: '1',
        title: 'second schedule',
        category: 'time',
//        dueDateClass: '',
        start: '2021-04-01T22:30:00+09:00',
        end: '2021-04-02T22:30:00+09:00',
    }
]);

cal.on('beforeCreateSchedule', function (event: ISchedule) {
    event.category = 'time';
    cal.createSchedules([event]);
});
