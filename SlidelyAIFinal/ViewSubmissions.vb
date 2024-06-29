Imports System.Net.Http
Imports System.Reflection
Imports Newtonsoft.Json

Public Class ViewSubmissions
    Private currentSubmissionIndex As Integer = 0
    Private submissions As List(Of Submission)

    Public Sub New()
        InitializeComponent()
        LoadSubmissions()
        DisplaySubmission(0)
    End Sub

    Private Async Sub LoadSubmissions()
        Using client As New HttpClient()
            Dim response = Await client.GetAsync("http://localhost:3000/read")
            If response.IsSuccessStatusCode Then
                Dim content = Await response.Content.ReadAsStringAsync()
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(content)
            Else
                MessageBox.Show("Failed to load submissions.")
            End If
        End Using
        Dim submission = submissions(0)
        txtName.Text = submission.name
        txtEmail.Text = submission.email
        txtPhone.Text = submission.phone
        txtGithub.Text = submission.github_link
        txtStopWatch.Text = submission.stopwatch_time
    End Sub

    Private Sub DisplaySubmission(index As Integer)
        If submissions Is Nothing OrElse index < 0 OrElse index >= submissions.Count Then
            Return
        End If
        Dim submission = submissions(index)
        txtName.Text = submission.name
        txtEmail.Text = submission.email
        txtPhone.Text = submission.phone
        txtGithub.Text = submission.github_link
        txtStopWatch.Text = submission.stopwatch_time
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentSubmissionIndex > 0 Then
            currentSubmissionIndex -= 1
            DisplaySubmission(currentSubmissionIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentSubmissionIndex < submissions.Count - 1 Then
            currentSubmissionIndex += 1
            DisplaySubmission(currentSubmissionIndex)
        End If
    End Sub

    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        End If
    End Sub
End Class
